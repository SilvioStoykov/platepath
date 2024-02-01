﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Services;
using System.Security.Claims;

namespace PlatePath.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MealPlansController : ControllerBase
    {
        readonly IEdamamService _edamamService;

        public MealPlansController(IEdamamService edamamService)
        {
            _edamamService = edamamService;
        }

        [HttpPost("generate")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GenerateMealPlan([FromBody] GenerateMealPlanRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return ValidationProblem();

            return Ok(await _edamamService.GenerateMealPlan(userId, request));
        }

        [HttpGet("{name}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetMealPlan(string name)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return ValidationProblem();

            return Ok(await _edamamService.GetMealPlan(userId, name));
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllMealPlans()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return ValidationProblem();

            return Ok(await _edamamService.GetAllMealPlans(userId));
        }

        [HttpPut("setMealCompletionStatus")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> SetMealCompletionStatus([FromBody] SetRecipeCompletionStatusRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return ValidationProblem();

            bool updated = await _edamamService.SetMealPlanRecipeCompletionStatus(
                request.MealPlanId, request.RecipeId, request.Completed);

            return Ok(updated);
        }

        [HttpGet("getMealCompletionStatuses")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetMealCompletionStatuses(int mealPlanId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return ValidationProblem();

            List<MealPlanRecipe> results = await _edamamService.GetMealPlanRecipeCompletionStatuses(mealPlanId);

            return Ok(results);
        }
    }
}