﻿using RSA_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSA_Web.Interfaces;

namespace RSA_Web.Services
{
    public class RSAConfigurationService : IServiceRSAConfiguration
    {
        public Configuration DefaultConfiguration { get; set; }

        public Configuration CurrentConfiguration { get; set; }

        private IServiceDirection DirectionService { get; set; }

        private bool IsMinimization { get; set; }

        public RSAConfigurationService(IServiceDirection DirectionService)
        {

            this.DirectionService = DirectionService;

            DefaultConfiguration = new Configuration()
            {
                FunctionArgumetnsCount = 2,
                StepsLimit = 300,
                MaxZeroPointValue = 0,
                MinZeroPointValue = -2,
                StepSize = 0.05,
                DirectionsCount = 100,
                IsMinimization = true,

                ZeroPoint = new double[2].ToList(),

                Function = (List<double> Args) =>
                {
                    return 3 * Math.Pow(Args[0], 2) + Args[0] * Args[1] + 2 * Math.Pow(Args[1], 2) - Args[0] - 4 * Args[1];                  
                },

                EvaluateSolutionQuality = (Step CurrentStep, double? CurrentExtremum) =>
                {
                    return (CurrentExtremum == null || this.CurrentConfiguration.ExtremumPredicate(CurrentStep.FunctionValue, CurrentExtremum));
                },

                IsStop = (Step CurrentStep) =>
                {
                    return 
                    (CurrentStep != null && (
                     CurrentStep?.StepNumber >= this.CurrentConfiguration.StepsLimit  || 
                     (CurrentStep?.Direction.Index >= DirectionService.Directions.Count - 1) && CurrentStep?.IsGoodSolution == false));
                },

                ExtremumPredicate = (double? left, double? right) =>
                {
                    if (IsMinimization)
                    {
                        return left < right;
                    }
                    else
                    {
                        return left > right;
                    }
                }
            };

            CurrentConfiguration = DefaultConfiguration;
        }

    }
}
