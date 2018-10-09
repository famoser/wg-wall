﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WgWall.Test.Controllers
{
    public class JsonHelper
    {
        public static void AssertBooleanResult(IActionResult result, bool expected)
        {
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.IsInstanceOfType(objectResult.Value, typeof(bool));
            Assert.AreEqual(expected, (bool)objectResult.Value);
        }
    }
}
