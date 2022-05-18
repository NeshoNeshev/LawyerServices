﻿using LaweyrServices.Web.Shared.LawyerViewModels;
using LaweyrServices.Web.Shared.RatingModels;
using LawyerServices.Services.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IRatingService ratingService;
        private readonly ICompanyService companyService;

        public ReviewController(IRatingService ratingService, ICompanyService companyService)
        {
            this.ratingService = ratingService;
            this.companyService = companyService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("GetAllRatings")]
        public async Task<IActionResult> GetAllRatings()
        { 
           var response = await this.ratingService.GetAllRatingsAsync();
           return Ok(response);
        }

        [Authorize(Roles = "User")]
        [HttpGet("ExistingReview")]
        public async Task<IActionResult> ExistingReview(string id)
        {
            var response = await this.ratingService.ExistingRatingAsync(id);
            return Ok(response);
        }
        [Authorize(Roles = "User")]
        [HttpGet("GetLawyer")]
        public async Task<IActionResult> GetLawyer(string lawyerId)
        {
            var response = this.companyService.GetLawyer<LawyerToReviewViewModel>(lawyerId);
            return Ok(response);
        }

        [Authorize(Roles = "User")]
        [HttpPost("CreateRatingAsync")]
        public async Task<IActionResult> CreateRatingAsync([FromBody] RatingInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.UserId = userId;
            var response = await this.ratingService.CreateRatingAsync(model);
            return Ok(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("CensoredRatingReview")]
        public async Task<IActionResult> CensoredRatingReview(string ratingId)
        {
            var response = await this.ratingService.CensoredRatingReviewAsync(ratingId);
            if (!response)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
