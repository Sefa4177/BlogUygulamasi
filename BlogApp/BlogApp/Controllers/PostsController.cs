using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
         private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private ITagRepository _tagRepository;
        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
        }
        public async Task<IActionResult> Index(string tag)
        {
            var claims = User.Claims;
            
            var posts = _postRepository.Posts.Include(x => x.Tags).Where(i => i.IsActive);

            if(!string.IsNullOrEmpty(tag))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            return View( new PostsViewModel { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> Details(string url)
        {
            return View(await _postRepository
                        .Posts
                        .Include(x => x.User)
                        .Include(x => x.Tags)
                        .Include(x => x.Comments)
                        .ThenInclude(x => x.User)
                        .FirstOrDefaultAsync(p => p.Url == url));
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string Text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);
            
            var entity = new Comment {
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                UserId = int.Parse(userId ?? "")

            };
            _commentRepository.CreateComment(entity);

            return Json(new { 
                username,
                Text,
                entity.PublishedOn,
                avatar
            });
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Tags = _tagRepository.Tags.ToList();
            return View();
        }

        [HttpPost]
        [Authorize]
        public  async Task <IActionResult> Create(PostCreateViewModel model, IFormFile Image,int[] tagIds)
        {
            if (ModelState.ContainsKey("Image"))
            {
                ModelState.Remove("Image");
            }
            var extension = "";
            if(Image != null)
            {
                var allowedExtensions = new[] {".jpg",".jpeg",".png"};
                extension = Path.GetExtension(Image.FileName);
                if(!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("","Sadece .jpg , .jpeg ve .png uzantılı resimler kabul edilmektedir");
                }
            }

            if(ModelState.IsValid)
            {
                if(Image != null)
                {
                    var randomFileName =string.Format($"{Guid.NewGuid().ToString()}{extension}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                    using(var stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    model.Image = randomFileName;

                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    _postRepository.CreatePost(
                        new Post{
                            Title = model.Title,
                            Description = model.Description,
                            Content = model.Content,
                            Url = model.Url,
                            UserId = int.Parse(userId ?? ""),
                            PublishedOn = DateTime.Now,
                            Image = model.Image,
                            IsActive = false
                        }
                    );
                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);
            
            var posts = _postRepository.Posts;

            if(string.IsNullOrEmpty(role))
            {
                posts = posts.Where(i => i.UserId == userId);
            }

            return View(await posts.ToListAsync());
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var post = _postRepository.Posts.Include(i=>i.Tags).FirstOrDefault(i=>  i.PostId == id);
            if(post == null)
            {
                return NotFound();
            }

            ViewBag.Tags = _tagRepository.Tags.ToList();

            return View(new PostEditViewModel {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                IsActive = post.IsActive,
                Tags = post.Tags
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PostEditViewModel model,IFormFile Image, int[] tagIds)
        {
            var post = await _postRepository.Posts.FirstOrDefaultAsync(i=>  i.PostId == model.PostId);

            if (ModelState.ContainsKey("Image"))
            {
                ModelState.Remove("Image");
            }

            var extension = "";
            if(Image != null)
            {
                var allowedExtensions = new[] {".jpg",".jpeg",".png"};
                extension = Path.GetExtension(Image.FileName);
                if(!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("","Sadece .jpg , .jpeg ve .png uzantılı resimler kabul edilmektedir");
                }
            }


            if(ModelState.IsValid)
            {
                if(Image != null)
                {
                    var randomFileName =string.Format($"{Guid.NewGuid().ToString()}{extension}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                    using(var stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    if(post != null)
                    {
                        string imagePath = $"wwwroot/img/{post.Image}";

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    model.Image = randomFileName;
                }
                var entityToUpdate = new Post {
                    PostId = model.PostId,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url,
                    Image = post.Image
                };

                if(Image != null)
                {
                    entityToUpdate.Image = model.Image;
                }

                if(User.FindFirstValue(ClaimTypes.Role) == "admin") 
                {
                    entityToUpdate.IsActive = model.IsActive;
                }

                _postRepository.EditPost(entityToUpdate,tagIds);
                return RedirectToAction("List");
            }
            ViewBag.Tags = _tagRepository.Tags.ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            
            var post = await _postRepository.Posts.FirstOrDefaultAsync(i=>  i.PostId == id);

            if(post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)
        {
           var post = await _postRepository.Posts.FirstOrDefaultAsync(i=>  i.PostId == id);
           if(post == null)
           {
            return NotFound();
           }
           string imagePath = $"wwwroot/img/{post.Image}";

           if (System.IO.File.Exists(imagePath))
           {
            System.IO.File.Delete(imagePath);
           }
           _postRepository.DeletePost(post);
           return RedirectToAction("List");
        }



    }
}