using System;
using FeedTheCrowd.Dtos.Comments;
using FeedTheCrowd.Models;
using AutoMapper;
using FeedTheCrowd.Dtos.Recipes;
using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Dtos.User;
using FeedTheCrowd.Dtos.Events;

namespace FeedTheCrowd.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration() : this("FeedTheCrowd")
        {

        }

        protected AutoMapperConfiguration(string name) 
        {

            CreateMap<NewCommentDto, Comment>().ForMember(i => i.FkUser, opt => opt.MapFrom(src => src.UserId))
                .ForMember(i => i.FkRecipe, opt => opt.MapFrom(src => src.RecipeId));
            CreateMap<Comment, NewCommentDto>().ForMember(i => i.UserId, opt => opt.MapFrom(src => src.FkUser))
                .ForMember(i => i.RecipeId, opt => opt.MapFrom(src => src.FkRecipe));
            CreateMap<AllCommentsDto, Comment>(MemberList.None);
            CreateMap<Comment, AllCommentsDto>().ForMember(i => i.User, opt => opt.MapFrom(src => src.FkUserNavigation.Username))
                .ForMember(i=> i.UserPic, opt => opt.MapFrom(src => src.FkUserNavigation.Photo));
            CreateMap<CommentDto, Comment>(MemberList.None);
            CreateMap<Comment, CommentDto>(MemberList.Destination);

            CreateMap<NewRecipeDto, Recipe>(MemberList.None);
            CreateMap<AllRecipesDto, Recipe>(MemberList.None);
            CreateMap<RecipeDto, Recipe>(MemberList.None);
            CreateMap<Recipe, RecipeDto>(MemberList.Destination);

            CreateMap<NewProductDto, Product>(MemberList.None);
            CreateMap<AllProductsDto, Product>(MemberList.None);
            CreateMap<ProductDto, Product>(MemberList.None);
            CreateMap<Product, ProductDto>(MemberList.Destination);

            CreateMap<User, UserDto>(MemberList.None);
            CreateMap<UserDto, User>(MemberList.None);
            CreateMap<AllUserDto, User>(MemberList.None);

            CreateMap<NewEventDto, Event>().ForMember(i => i.FkUser, opt => opt.MapFrom(src => src.UserId));
            CreateMap<Event, EventDto>().ForMember(i => i.UserId, opt => opt.MapFrom(src => src.FkUser));

        }
    }
}
