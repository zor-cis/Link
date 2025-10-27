using AutoMapper;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Core.Applicacion.ViewModel.Generic;
using LinkUp.Core.Applicacion.ViewModel.Home;
using LinkUp.Core.Applicacion.ViewModel.PostCommen;
using LinkUp.Core.Applicacion.ViewModel.Publication;
using LinkUp.Core.Applicacion.ViewModel.Reaction;
using LinkUp.Core.Applicacion.ViewModel.Reply;
using LinkUp.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkUp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPublicationService _servicePublications;
        private readonly IPostCommenService _postCommenService;
        private readonly IReplyService _replyService;
        private readonly IMapper _mapper;
        private readonly IReactionService _reactionService;

        public HomeController(UserManager<AppUser> userManager, IPublicationService servicePublications, IMapper mapper, IPostCommenService postCommenService, IReplyService replyService, IReactionService reactionService)
        {
            _userManager = userManager;
            _servicePublications = servicePublications;
            _mapper = mapper;
            _postCommenService = postCommenService;
            _replyService = replyService;
            _reactionService = reactionService;
        }

        public async Task<IActionResult> Index()
        {
            AppUser? userSession = await _userManager.GetUserAsync(User);

            if (userSession == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var publications = await _servicePublications.GetPublicationsByUserIdAsync(userSession.Id);
            
            var homeView = new HomeViewModel
            {
                Publication = _mapper.Map<List<PublicationViewModel>>(publications!.Result),

                CreateCommen = new CreatePostCommenViewModel
                {
                    IdUser = userSession.Id,
                    IdPublication = 0,
                    Id = 0,
                    Text = ""
                }, 

                CreateReply = new CreateReplyViewModel 
                { 
                    Id = 0,
                    IdPostComment = 0, 
                    IdUser = userSession.Id,
                    ReplyComment = ""
                }, 

                CreateReaction = new CreateReactionViewModel 
                {
                    Id = 0,
                    IdPublication = 0,
                    IdUser = userSession.Id,
                    ReactionType = 0
                },

                DeleteView = new DeleteViewModel 
                { 
                    Id = 0,
                }

            };

            foreach (var publication in homeView.Publication) 
            {
                var comments = await _postCommenService.GetAllByPublicationAsync(publication.Id);
                var reactions = await _reactionService.GetByPublicationtAsync(publication.Id);

                if(comments != null && !comments.IsError)
                {
                    var commentsView= _mapper.Map<List<PostCommenViewModel>>(comments.Result);
                    foreach(var comment in commentsView) 
                    { 
                        var replies = await _replyService.GetAllByCommentAsync(comment.Id);
                        if(replies != null && !replies.IsError) 
                        {
                            comment.ReplyCommen = _mapper.Map<List<ReplyViewModel>>(replies.Result);
                        }
                    }

                    publication.PostCommen = commentsView;
                }

                if(reactions != null && !reactions.IsError) 
                { 
                    var reactionView = _mapper.Map<ReactionViewModel>(reactions.Result);
                    publication.Reaction = reactionView;
                }
            }

        
            return View("Index", homeView);
        }

        
    }
}
