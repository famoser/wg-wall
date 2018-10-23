using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WgWall.Api.Dto;
using WgWall.Api.Dto.Base;
using WgWall.Controllers.Base;
using WgWall.Data.Model.Base;

namespace WgWall.Test.Controllers.Base
{
    public class HideableCrudControllerTest<TEntity, TDto, TPayload>
        where TEntity : BaseEntity, new()
        where TDto : BaseDto
    {
        private readonly HideableCrudController<TEntity, TDto, TPayload> _controller;

        public HideableCrudControllerTest(HideableCrudController<TEntity, TDto, TPayload> controller)
        {
            _controller = controller;
        }

        [TestMethod]
        public async Task Get_ShouldReturnAppropriateSampleData()
        {
            //arrange
            var firstResult = await _controller.Get();
            var firstList = AssertHelper.AssertList<TDto>(firstResult);
            var entityToHide = firstList[0];

            //act
            await _controller.Hide(entityToHide.Id);
            var secondResult = await _controller.Get();
            var secondList = AssertHelper.AssertList<TDto>(secondResult);
            var doneProd = secondList.FirstOrDefault(p => p.Id == entityToHide.Id);

            //assert
            Assert.IsNull(doneProd);
            Assert.IsTrue(secondList.Count == firstList.Count);
        }

        [TestMethod]
        public async Task Hide_ShouldExcludeEntityFromGetResult()
        {
            //arrange
            var firstResult = await _controller.Get();
            var firstList = AssertHelper.AssertList<TDto>(firstResult);
            var entityToHide = firstList[0];

            //act
            await _controller.Hide(entityToHide.Id);
            var secondResult = await _controller.Get();
            var secondList = AssertHelper.AssertList<TDto>(secondResult);
            var doneProd = secondList.FirstOrDefault(p => p.Id == entityToHide.Id);

            //assert
            Assert.IsNull(doneProd);
            Assert.IsTrue(secondList.Count == firstList.Count);
        }
    }
}
