﻿using RSA_Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RSA_Web.ViewModels
{
    [Display(Name ="Решение")]
    public class StepView
    {
        [Display(Name = "Номер шага")]
        public int StepNumber { get; set; }

        [Display(Name = "Размер шага")]
        public double StepSize { get; set; }

        [Display(Name = "Направление")]
        public DirectionView Direction { get; set; }

        [Display(Name = "Точка")]
        public List<double> Point { get; set; }

        [Display(Name = "Значение функции")]
        public double FunctionValue { get; set; }

        [Display(Name = "Оценка решения")]
        public bool IsGoodSolution { get; set; }

        [Display(Name = "Выполнение критерия остановки")]
        public bool IsFinalStep { get; set; }

        public static implicit operator StepView(Step step)
        {
            return new StepView()
            {
                StepNumber = step.StepNumber,
                StepSize = step.StepSize,
                Direction = step.Direction,
                Point = step.Point,
                FunctionValue = step.FunctionValue,
                IsGoodSolution = step.IsGoodSolution,
                IsFinalStep = step.IsFinalStep
            };
        }
    }
}
