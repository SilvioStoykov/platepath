﻿using PlatePath.API.Data.Models.MealPlans;

namespace PlatePath.API.Services;

public interface IEdamamService
{
    Task<GenerateMealPlanResponse> GenerateMealPlan(string userId, GenerateMealPlanRequest request);
    Task<MealPlanResponse> GetMealPlan(string userId, string name);
    Task<AllMealPlansResponse> GetAllMealPlans(string userId);
    Task<bool> SetMealPlanRecipeCompletionStatus(int mealPlanId, int recipeId, bool completed);
    Task<List<MealPlanRecipe>> GetMealPlanRecipeCompletionStatuses(int mealPlanId);
}