using BAL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using WebLNCTAssistant.Areas.FeedBack.Models;

namespace WebLNCTAssistant.Areas.FeedBack.Controllers
{
    [Area("FeedBack")]
    [Route("FeedBack")]
    public class FeedBackController : Controller
    {
        [HttpGet]
        [Route("CreateFeedBack")]
        public async Task<IActionResult> CreateFeedBack()
        {
            try
            {
                FeedBackModel objFeedBackModel = new FeedBackModel();
                return View(objFeedBackModel);
            }
            catch(Exception ex)
            {
                return RedirectToAction("FailureMessage");
            }
        }

        [HttpPost]
        [Route("CreateFeedBack")]
        public async Task<IActionResult> CreateFeedBack(FeedBackModel objFeedBackModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    objFeedBackModel.IsShowValidation = true;
                    objFeedBackModel.ErrorMessage = "Validate the models.";
                    return View(objFeedBackModel);
                }

                FeedBackDTO objFeedBackDTO = new FeedBackDTO()
                {
                    Name = objFeedBackModel.Name,
                    Email = objFeedBackModel.Email,
                    Mobile = objFeedBackModel.Mobile,
                    FeedBackType = objFeedBackModel.FeedBackType,
                    Subject = objFeedBackModel.Subject,
                    FeedBackMessage = objFeedBackModel.FeedBackMessage,
                    Priority_Level = objFeedBackModel.Priority_Level,
                    Status = "PENDING",
                };

                objFeedBackDTO = await BALFactory.Instance.FeedBackBAL.InsertFeedBack(objFeedBackDTO);

                if (objFeedBackDTO.Done)
                {
                    return RedirectToAction("SuccessMessage");
                }
                else
                {
                    return RedirectToAction("FailureMessage");
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction("FailureMessage");

            }
        }

        [HttpGet]
        [Route("GetAllFeedBack")]
        public async Task<IActionResult> GetAllFeedBack()
        {
            try
            {
                List<FeedBackDTO> lstFeedBackDTO = await BALFactory.Instance.FeedBackBAL.GetAllFeedBack();
                if(lstFeedBackDTO != null)
                {
                    return View(lstFeedBackDTO);
                }
                else
                {
                    return RedirectToAction("FailureMessage");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("FailureMessage");
            }
        }

        [HttpGet]
        [Route("EditFeedBack")]
        public async Task<IActionResult> EditFeedBack(long feedbackid1)
        {
            try
            {
                FeedBackDTO objFeedBackDTO = await BALFactory.Instance.FeedBackBAL.GetFeedBackByID(feedbackid1);
                FeedBackModel objFeedBackModel = new FeedBackModel()
                {
                    FeedBackID = objFeedBackDTO.FeedBackID,
                    Name = objFeedBackDTO.Name,
                    Email = objFeedBackDTO.Email,
                    Mobile = objFeedBackDTO.Mobile,
                    FeedBackType = objFeedBackDTO.FeedBackType,
                    Subject = objFeedBackDTO.Subject,
                    FeedBackMessage = objFeedBackDTO.FeedBackMessage,
                    Priority_Level = objFeedBackDTO.Priority_Level,
                    Status = objFeedBackDTO.Status, 
                };
                if (objFeedBackModel != null)
                {
                    return View(objFeedBackModel);
                }
                else
                {
                    return RedirectToAction("FailureMessage");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("FailureMessage");
            }
        }

        [HttpPost]
        [Route("EditFeedBack")]
        public async Task<IActionResult> EditFeedBack(FeedBackModel objFeedBackModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    objFeedBackModel.IsShowValidation = true;
                    objFeedBackModel.ErrorMessage = "Validate the models.";
                    return View(objFeedBackModel);
                }

                FeedBackDTO objFeedBackDTO = new FeedBackDTO()
                {
                    FeedBackID = objFeedBackModel.FeedBackID,
                    Name = objFeedBackModel.Name,
                    Email = objFeedBackModel.Email,
                    Mobile = objFeedBackModel.Mobile,
                    FeedBackType = objFeedBackModel.FeedBackType,
                    Subject = objFeedBackModel.Subject,
                    FeedBackMessage = objFeedBackModel.FeedBackMessage,
                    Priority_Level = objFeedBackModel.Priority_Level,
                    Status = objFeedBackModel.Status,
                };
                objFeedBackDTO = await BALFactory.Instance.FeedBackBAL.UpdateFeedBack(objFeedBackDTO);
                if (objFeedBackDTO.Done)
                {
                    return RedirectToAction("EditSuccessMessage");

                }
                else
                {
                    return RedirectToAction("FailureMessage");
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction("FailureMessage");
            }
        }

        [HttpGet]
        [Route("FeedBackMessageDetail")]
        public async Task<IActionResult> FeedBackMessageDetail(long feedbackid1)
        {
            try
            {
                FeedBackDTO objFeedBackDTO = await BALFactory.Instance.FeedBackBAL.GetFeedBackByID(feedbackid1);
                FeedBackModel objFeedBackModel = new FeedBackModel()
                {
                    FeedBackID = objFeedBackDTO.FeedBackID,
                    Name = objFeedBackDTO.Name,
                    Email = objFeedBackDTO.Email,
                    Mobile = objFeedBackDTO.Mobile,
                    FeedBackType = objFeedBackDTO.FeedBackType,
                    Subject = objFeedBackDTO.Subject,
                    FeedBackMessage = objFeedBackDTO.FeedBackMessage,
                    Priority_Level = objFeedBackDTO.Priority_Level,
                    Status = objFeedBackDTO.Status,
                };
                if (objFeedBackModel != null)
                {
                    return PartialView("_FeedBackMessageDetail", objFeedBackModel);
                }
                else
                {
                    return RedirectToAction("FailureMessage");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("FailureMessage");

            }
        }


        [HttpGet]
        [Route("EditSuccessMessage")]
        public async Task<IActionResult> EditSuccessMessage()
        {
            return View("_EditSuccessMessage");
        }

        [HttpGet]
        [Route("SuccessMessage")]
        public async Task<IActionResult> SuccessMessage()
        {
            return View("_SuccessMessage");
        }

        [HttpGet]
        [Route("FailureMessage")]
        public async Task<IActionResult> FailureMessage()
        {
            return View("_FailureMessage");
        }
    }
}
