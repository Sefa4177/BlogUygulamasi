using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if(context != null)
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if(!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag { Text = "Bitki", Url = "bitki", Color = TagColors.warning },
                        new Tag { Text = "Hayvan", Url="hayvan", Color = TagColors.info },
                        new Tag { Text = "Uzay", Url="uzay" , Color = TagColors.succes },
                        new Tag { Text = "Deniz", Url="deniz", Color = TagColors.secondary  },
                        new Tag { Text = "Araba", Url="araba", Color = TagColors.primary  },
                        new Tag { Text = "Kedi", Url="kedi", Color = TagColors.succes  },
                        new Tag { Text = "Tekne", Url="tekne", Color = TagColors.secondary  },
                        new Tag { Text = "Köpek", Url="köpek", Color = TagColors.secondary  },
                        new Tag { Text = "Canlılar", Url="canlılar", Color = TagColors.secondary },
                        new Tag { Text = "Araç", Url="araç", Color = TagColors.secondary  },
                        new Tag { Text = "Kuş", Url="kuş", Color = TagColors.secondary  }



                    );
                    context.SaveChanges();
                }

                if(!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "sefacanpehlivan", Name = "Sefa Can Pehlivan", Email = "sefa@gmail.com", Password="123456", Image = "avatar1.jpg"},
                        new User { UserName = "ahmet123", Name = "Ahmet Aslan", Email = "ahmet@gmail.com", Password="123456", Image = "avatar2.jpg"}
                    );
                    context.SaveChanges();
                }

                if(!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post {
                            Title = "Fide Yetiştirme Süreci",
                            Description = "Nulla vitae nisi sit amet lorem cursus ultrices. Mauris dictum leo velit, quis mollis nisl sagittis a. Donec luctus ornare mattis. Sed lobortis, arcu in placerat tristique, dolor dolor congue risus, mattis dapibus sapien lorem in risus. Sed vestibulum urna quis feugiat fringilla. Suspendisse dictum non nibh commodo pulvinar. Quisque at mauris odio. Vestibulum venenatis leo et tempus sagittis.",
                            Content = "Nulla vitae nisi sit amet lorem cursus ultrices. Mauris dictum leo velit, quis mollis nisl sagittis a. Donec luctus ornare mattis. Sed lobortis, arcu in placerat tristique, dolor dolor congue risus, mattis dapibus sapien lorem in risus. Sed vestibulum urna quis feugiat fringilla. Suspendisse dictum non nibh commodo pulvinar. Quisque at mauris odio. Vestibulum venenatis leo et tempus sagittis.Mauris efficitur nec ipsum vel mollis. Aliquam erat volutpat. Cras luctus est eu nunc pellentesque, nec porta libero tristique. In hac habitasse platea dictumst. Cras mollis lacus malesuada neque vestibulum lobortis. Morbi sollicitudin nibh sit amet lacus tincidunt, ut fringilla neque laoreet. Nullam finibus ultrices tempor. Proin finibus ac nunc et semper. Ut interdum ante a nisi euismod, at fermentum odio volutpat. Praesent porta sapien non mi euismod molestie vel sit amet lectus. Donec mattis, orci suscipit vehicula commodo, lorem mi posuere sem, eu dignissim ex diam condimentum odio. Pellentesque pulvinar ipsum sagittis, viverra justo quis, tempus dui. Donec feugiat nec turpis vitae finibus. Duis eu facilisis est, vitae mattis ligula. Morbi id venenatis leo. Maecenas diam lectus, efficitur a dignissim a, semper quis mi. Vivamus condimentum, dui sit amet consequat maximus, odio augue tempus nunc, in tincidunt nulla ante ac risus. Quisque quis venenatis justo, ac rutrum eros. Etiam vel blandit nulla, non dignissim massa. Nulla tincidunt, purus at rhoncus sodales, odio nibh finibus dui, at scelerisque est felis sed nisi. Duis id enim pretium, facilisis risus fringilla, vestibulum nulla. Aenean feugiat quis nulla a eleifend. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Quisque lobortis sem ac commodo facilisis. Fusce commodo arcu eu sodales suscipit. Sed ex nunc, dignissim nec odio ac, condimentum consectetur nulla. Morbi finibus non lectus ac porttitor. Aenean hendrerit vel mauris a vehicula. Aliquam massa turpis, posuere vel lorem id, dignissim iaculis ex.",
                            Url = "fide-yetistirme-süreci",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(1).ToList(),
                            Image = "fide.jpg",
                            UserId = 1,
                            Comments = new List<Comment> { 
                                new Comment { Text = "Güzel anlatım için teşekkürler", PublishedOn = DateTime.Now.AddDays(-10), UserId = 2}
                            }
                            
                        },
                        new Post {
                            Title = "Kedi Bakımı Hakkında Bilinmesi Gerekenler",
                            Description = "In sit amet leo tellus. Nam lacinia magna non dui sagittis, nec tempus est facilisis. Cras tristique sed magna vel facilisis. Cras viverra ex at ullamcorper vulputate. Praesent varius, erat quis tristique imperdiet, orci quam efficitur urna, quis placerat dui eros at augue. Praesent rhoncus semper tellus, ut ultricies dolor rutrum sollicitudin. In vel mauris aliquam, tristique ex id, congue arcu. Duis tincidunt ligula a pulvinar mollis. Sed ultrices erat eu tristique dignissim. Praesent porta vestibulum tempus. Integer sit amet pulvinar nibh.",
                            Content = "In sit amet leo tellus. Nam lacinia magna non dui sagittis, nec tempus est facilisis. Cras tristique sed magna vel facilisis. Cras viverra ex at ullamcorper vulputate. Praesent varius, erat quis tristique imperdiet, orci quam efficitur urna, quis placerat dui eros at augue. Praesent rhoncus semper tellus, ut ultricies dolor rutrum sollicitudin. In vel mauris aliquam, tristique ex id, congue arcu. Duis tincidunt ligula a pulvinar mollis. Sed ultrices erat eu tristique dignissim. Praesent porta vestibulum tempus. Integer sit amet pulvinar nibh. Sed varius in neque ac fringilla. Pellentesque eget arcu finibus, maximus velit non, sollicitudin arcu. Curabitur elit lacus, venenatis vel turpis vel, lacinia finibus risus. Mauris suscipit eget mi eu aliquam. Fusce iaculis tellus bibendum erat pellentesque, sed mollis enim vestibulum Aenean at ullamcorper arcu. Suspendisse egestas placerat dignissim. Suspendisse in velit non sem malesuada dignissim. Nullam volutpat tellus vel nisl semper, a rhoncus augue condimentum. Vivamus a leo sagittis, posuere nibh vitae, vestibulum justo. Integer at faucibus orci, finibus vehicula diam. Sed et purus ante. Curabitur a ex bibendum, ultrices purus in, eleifend lacus. Aliquam nec dignissim felis, ac imperdiet tellus.",
                            Url = "kedi-bakimi-hakkında-bilinmesi-gerekenler",
                            IsActive = true,
                            Image = "kedi.jpeg",
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(2).ToList(),
                            UserId = 1
                        },
                        new Post {
                            Title = "Gezegenler Hakkında Az Bilinenler",
                            Description = "Nulla ultricies dolor sed nibh elementum eleifend. Phasellus tincidunt auctor justo, nec iaculis purus rutrum eget. Aliquam bibendum sit amet ante at lobortis. In pulvinar bibendum nibh, sit amet vestibulum ante vehicula ut. Proin vel nibh congue diam aliquam rutrum. Aliquam vehicula nibh eu odio vehicula, in gravida ante finibus. Vivamus luctus et leo sed elementum. Pellentesque elit arcu, posuere in porttitor at, vestibulum quis neque.",
                            Content = "Nulla ultricies dolor sed nibh elementum eleifend. Phasellus tincidunt auctor justo, nec iaculis purus rutrum eget. Aliquam bibendum sit amet ante at lobortis. In pulvinar bibendum nibh, sit amet vestibulum ante vehicula ut. Proin vel nibh congue diam aliquam rutrum. Aliquam vehicula nibh eu odio vehicula, in gravida ante finibus. Vivamus luctus et leo sed elementum. Pellentesque elit arcu, posuere in porttitor at, vestibulum quis neque.Sed eu mi risus. Phasellus vehicula finibus facilisis. Integer mollis, justo id interdum placerat, quam orci egestas massa, sed gravida est ex id sem. Donec tincidunt, leo at sollicitudin sagittis, ante nunc vehicula ligula, vel dignissim tellus ex nec odio. Nam tempus metus accumsan consectetur aliquam. Quisque dapibus quam ac dapibus tincidunt. Donec semper, nunc nec consectetur ornare, risus enim volutpat purus, id condimentum sem purus eu quam. Sed dapibus molestie tortor, ut vestibulum ipsum bibendum sit amet. Mauris sem mauris, malesuada at vehicula dapibus, fermentum at ante.Nunc at risus eu neque congue imperdiet. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed et ipsum ex. Donec nunc ante, accumsan in aliquam ut, convallis sit amet augue. Etiam in elit vel est pharetra fermentum. Sed mollis odio risus, at pellentesque ante tincidunt sed. Etiam ornare sed arcu ut imperdiet.",
                            Url = "gezegenler-hakkında-az-bilinenler",
                            IsActive = true,
                            Image = "gezegenler.jpg",
                            PublishedOn = DateTime.Now.AddDays(-30),
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 2
                        }
                        ,
                        new Post {
                            Title = "Zıplayan Araba Dikkarleri Üzerine Çekti",
                            Description = "Suspendisse vel est sed orci mollis vehicula a ut sem. Sed leo enim, rutrum et risus sit amet, efficitur semper sem. Sed in purus vitae lacus suscipit volutpat et at ex. Fusce efficitur tempus magna. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec vel ante eget arcu auctor tristique nec sed tellus. Mauris aliquam, sem scelerisque facilisis rutrum, felis massa rhoncus erat, id auctor dolor risus sit amet lacus. Ut vel enim semper, blandit nulla eu, porttitor ipsum. Proin ipsum sem, lobortis vitae turpis eu, fringilla consectetur nisi. Fusce mollis lorem posuere, molestie nibh a, elementum nibh. Sed enim ante, dictum vitae ligula ut, malesuada auctor purus.",
                            Content = "Suspendisse vel est sed orci mollis vehicula a ut sem. Sed leo enim, rutrum et risus sit amet, efficitur semper sem. Sed in purus vitae lacus suscipit volutpat et at ex. Fusce efficitur tempus magna. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec vel ante eget arcu auctor tristique nec sed tellus. Mauris aliquam, sem scelerisque facilisis rutrum, felis massa rhoncus erat, id auctor dolor risus sit amet lacus. Ut vel enim semper, blandit nulla eu, porttitor ipsum. Proin ipsum sem, lobortis vitae turpis eu, fringilla consectetur nisi. Fusce mollis lorem posuere, molestie nibh a, elementum nibh. Sed enim ante, dictum vitae ligula ut, malesuada auctor purus.Donec ultrices vehicula metus, vitae mattis velit tempus eu. Integer et elementum ligula. Nulla venenatis ultrices nibh vel pulvinar. Morbi sed risus in elit eleifend tempus. Praesent nisi enim, egestas ac nulla id, accumsan luctus leo. Aenean lobortis tincidunt odio, a tempor ex blandit at. Phasellus risus erat, euismod eu fringilla non, vulputate vitae elit. Morbi fringilla viverra metus, vitae faucibus velit mollis id.Fusce a urna tincidunt, interdum sapien aliquam, dapibus libero. Nunc a elit eu tortor ultricies condimentum ac vel dolor. Sed vel ligula id nisl facilisis posuere vel in risus. Vivamus nec gravida est, placerat pulvinar metus. Integer porttitor erat eu pretium malesuada. Phasellus augue nulla, consectetur id aliquam vitae, scelerisque at ante. Quisque vitae feugiat arcu. Nam nisi odio, faucibus at elit finibus, consectetur aliquet mauris.",
                            Url = "zıplayan-araba-dikkatleri-üzerine-cekti",
                            IsActive = true,
                            Image = "araba.jpg",
                            PublishedOn = DateTime.Now.AddDays(-40),
                            Tags = context.Tags.Take(5).ToList(),
                            UserId = 2
                        }
                        ,
                        new Post {
                            Title = "Tekne Severlerin Favorisi",
                            Description = "Donec nec porta eros, ut feugiat nisl. Cras id eros nunc. Etiam dapibus lectus et libero convallis posuere. In hac habitasse platea dictumst. Ut lobortis ex et sapien convallis ultricies. Suspendisse semper lectus ac maximus consequat. Aliquam scelerisque orci nulla, eget consectetur nisi sagittis in. Quisque quis eros pulvinar, finibus quam at, malesuada tortor.",
                            Content = "Donec nec porta eros, ut feugiat nisl. Cras id eros nunc. Etiam dapibus lectus et libero convallis posuere. In hac habitasse platea dictumst. Ut lobortis ex et sapien convallis ultricies. Suspendisse semper lectus ac maximus consequat. Aliquam scelerisque orci nulla, eget consectetur nisi sagittis in. Quisque quis eros pulvinar, finibus quam at, malesuada tortor.Vestibulum fermentum lacus ligula, vitae accumsan justo fermentum ut. Pellentesque condimentum, nisl at iaculis malesuada, libero felis ultrices tellus, sit amet imperdiet orci magna non ipsum. Suspendisse tincidunt commodo sollicitudin. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed efficitur sollicitudin nulla quis venenatis. Sed pulvinar ante velit, eu sagittis sem mollis eget. Nulla sagittis, nunc a tincidunt dignissim, tellus sem bibendum justo, sed ornare sapien lacus aliquet sem. Aliquam mollis, ante et fringilla pulvinar, diam nunc pharetra magna, in iaculis lacus eros sit amet erat.Donec vulputate neque eget tempor blandit. Integer vitae interdum lectus, ut sollicitudin odio. Pellentesque dapibus neque ipsum, et luctus massa viverra at. Vivamus vel sodales ligula. Quisque semper leo quis leo tempor, ut rutrum justo ultricies. Integer et arcu vel erat dictum aliquam. Nullam ligula tellus, luctus nec libero quis, hendrerit imperdiet justo. Morbi nec tortor nisl.",
                            Url = "tekne-severlerin-favorisi",
                            IsActive = true,
                            Image = "tekne.jpg",
                            PublishedOn = DateTime.Now.AddDays(-50),
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 2
                        }
                        ,
                        new Post {
                            Title = "Muhabbet Kuşları Hakkında Az Bilinenler",
                            Description = "Donec nec porta eros, ut feugiat nisl. Cras id eros nunc. Etiam dapibus lectus et libero convallis posuere. In hac habitasse platea dictumst. Ut lobortis ex et sapien convallis ultricies. Suspendisse semper lectus ac maximus consequat. Aliquam scelerisque orci nulla, eget consectetur nisi sagittis in. Quisque quis eros pulvinar, finibus quam at, malesuada tortor.",
                            Content = "Donec nec porta eros, ut feugiat nisl. Cras id eros nunc. Etiam dapibus lectus et libero convallis posuere. In hac habitasse platea dictumst. Ut lobortis ex et sapien convallis ultricies. Suspendisse semper lectus ac maximus consequat. Aliquam scelerisque orci nulla, eget consectetur nisi sagittis in. Quisque quis eros pulvinar, finibus quam at, malesuada tortor.Vestibulum fermentum lacus ligula, vitae accumsan justo fermentum ut. Pellentesque condimentum, nisl at iaculis malesuada, libero felis ultrices tellus, sit amet imperdiet orci magna non ipsum. Suspendisse tincidunt commodo sollicitudin. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed efficitur sollicitudin nulla quis venenatis. Sed pulvinar ante velit, eu sagittis sem mollis eget. Nulla sagittis, nunc a tincidunt dignissim, tellus sem bibendum justo, sed ornare sapien lacus aliquet sem. Aliquam mollis, ante et fringilla pulvinar, diam nunc pharetra magna, in iaculis lacus eros sit amet erat.Donec vulputate neque eget tempor blandit. Integer vitae interdum lectus, ut sollicitudin odio. Pellentesque dapibus neque ipsum, et luctus massa viverra at. Vivamus vel sodales ligula. Quisque semper leo quis leo tempor, ut rutrum justo ultricies. Integer et arcu vel erat dictum aliquam. Nullam ligula tellus, luctus nec libero quis, hendrerit imperdiet justo. Morbi nec tortor nisl.",
                            Url = "muhabbet-kusları-hakkında-az-bilinenler",
                            IsActive = true,
                            Image = "kus.jpg",
                            PublishedOn = DateTime.Now.AddDays(-60),
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 2
                        }
                        ,
                        new Post {
                            Title = "Köpek Eğitimi Hakkında Bilgiler",
                            Description = "Nulla ultricies dolor sed nibh elementum eleifend. Phasellus tincidunt auctor justo, nec iaculis purus rutrum eget. Aliquam bibendum sit amet ante at lobortis. In pulvinar bibendum nibh, sit amet vestibulum ante vehicula ut. Proin vel nibh congue diam aliquam rutrum. Aliquam vehicula nibh eu odio vehicula, in gravida ante finibus. Vivamus luctus et leo sed elementum. Pellentesque elit arcu, posuere in porttitor at, vestibulum quis neque.",
                            Content = "Nulla ultricies dolor sed nibh elementum eleifend. Phasellus tincidunt auctor justo, nec iaculis purus rutrum eget. Aliquam bibendum sit amet ante at lobortis. In pulvinar bibendum nibh, sit amet vestibulum ante vehicula ut. Proin vel nibh congue diam aliquam rutrum. Aliquam vehicula nibh eu odio vehicula, in gravida ante finibus. Vivamus luctus et leo sed elementum. Pellentesque elit arcu, posuere in porttitor at, vestibulum quis neque.Sed eu mi risus. Phasellus vehicula finibus facilisis. Integer mollis, justo id interdum placerat, quam orci egestas massa, sed gravida est ex id sem. Donec tincidunt, leo at sollicitudin sagittis, ante nunc vehicula ligula, vel dignissim tellus ex nec odio. Nam tempus metus accumsan consectetur aliquam. Quisque dapibus quam ac dapibus tincidunt. Donec semper, nunc nec consectetur ornare, risus enim volutpat purus, id condimentum sem purus eu quam. Sed dapibus molestie tortor, ut vestibulum ipsum bibendum sit amet. Mauris sem mauris, malesuada at vehicula dapibus, fermentum at ante.Nunc at risus eu neque congue imperdiet. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed et ipsum ex. Donec nunc ante, accumsan in aliquam ut, convallis sit amet augue. Etiam in elit vel est pharetra fermentum. Sed mollis odio risus, at pellentesque ante tincidunt sed. Etiam ornare sed arcu ut imperdiet.",
                            Url = "kopek-egitimi-hakkında-bilgiler",
                            IsActive = true,
                            Image = "kopek.jpg",
                            PublishedOn = DateTime.Now.AddDays(-70),
                            Tags = context.Tags.Take(3).ToList(),
                            UserId = 1
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}