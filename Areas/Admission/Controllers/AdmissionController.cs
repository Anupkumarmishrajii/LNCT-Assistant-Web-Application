using BAL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using WebLNCTAssistant.Areas.Admission.Models;

namespace WebLNCTAssistant.Areas.Admission.Controllers
{
    [Area("Admission")]
    [Route("Admission")]
    public class AdmissionController : Controller
    {
        [HttpGet]
        [Route("CreateAdmission")]
        public async Task<IActionResult> CreateAdmission()
        {
            try
            {
                AdmissionModel objAdmissionModel = new AdmissionModel();
                return View(objAdmissionModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("FailureMessage");

            }
        }

        [HttpPost]
        [Route("CreateAdmission")]
        public async Task<IActionResult> CreateAdmission(AdmissionModel objAdmissionModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    objAdmissionModel.IsShowValidation = true;
                    objAdmissionModel.ErrorMessage = "Validate the mode";
                    return View(objAdmissionModel);
                }

                AdmissionDTO objAdmissionDTO = new AdmissionDTO()
                {
                    Name = objAdmissionModel.Name,
                    Email = objAdmissionModel.Email,
                    DOB = objAdmissionModel.DOB,
                    Gender = objAdmissionModel.Gender,
                    Disability = objAdmissionModel.Disability ? 1 : 0,
                    Address = objAdmissionModel.Address,
                    Mobile = objAdmissionModel.Mobile,
                    AlternateMobile = objAdmissionModel.AlternateMobile,
                    Previous_Institute = objAdmissionModel.Previous_Institute,
                    Year_PassedOut = objAdmissionModel.Year_PassedOut,
                    Score = objAdmissionModel.Score,
                    Desired_Program = objAdmissionModel.Desired_Program,
                    Language = objAdmissionModel.Language,
                    Reference = objAdmissionModel.Reference,
                    Status = "PENDING",

                };

                objAdmissionDTO = await BALFactory.Instance.AdmissionBAL.InsertAdmission(objAdmissionDTO);

                if (objAdmissionDTO.Done)
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
        [Route("EditAdmission")]
        public async Task<IActionResult> EditAdmission(long admissionid1)
        {
            try
            {
                AdmissionDTO objAdmissionDTO = await BALFactory.Instance.AdmissionBAL.GetAdmissionByID(admissionid1);
                AdmissionModel objAdmissionModel = new AdmissionModel()
                {
                    AdmissionID = objAdmissionDTO.AdmissionID,
                    Name = objAdmissionDTO.Name,
                    Email = objAdmissionDTO.Email,
                    DOB = objAdmissionDTO.DOB,
                    Gender = objAdmissionDTO.Gender,
                    Disability = objAdmissionDTO.Disability > 1 ? true : false,
                    Address = objAdmissionDTO.Address,
                    Mobile = objAdmissionDTO.Mobile,
                    AlternateMobile = objAdmissionDTO.AlternateMobile,
                    Previous_Institute = objAdmissionDTO.Previous_Institute,
                    Year_PassedOut = objAdmissionDTO.Year_PassedOut,
                    Score = objAdmissionDTO.Score,
                    Desired_Program = objAdmissionDTO.Desired_Program,
                    Language = objAdmissionDTO.Language,
                    Reference = objAdmissionDTO.Reference,
                    Status = objAdmissionDTO.Status,
                };
                if (objAdmissionModel != null)
                {
                    return View(objAdmissionModel);
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
        [Route("EditAdmission")]
        public async Task<IActionResult> EditAdmission(AdmissionModel objAdmissionModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    objAdmissionModel.IsShowValidation = true;
                    objAdmissionModel.ErrorMessage = "Validate the mode";
                    return View(objAdmissionModel);
                }

                AdmissionDTO objAdmissionDTO = new AdmissionDTO()
                {
                    AdmissionID = objAdmissionModel.AdmissionID,
                    Name = objAdmissionModel.Name,
                    Email = objAdmissionModel.Email,
                    DOB = objAdmissionModel.DOB,
                    Gender = objAdmissionModel.Gender,
                    Disability = objAdmissionModel.Disability ? 1 : 0,
                    Address = objAdmissionModel.Address,
                    Mobile = objAdmissionModel.Mobile,
                    AlternateMobile = objAdmissionModel.AlternateMobile,
                    Previous_Institute = objAdmissionModel.Previous_Institute,
                    Year_PassedOut = objAdmissionModel.Year_PassedOut,
                    Score = objAdmissionModel.Score,
                    Desired_Program = objAdmissionModel.Desired_Program,
                    Language = objAdmissionModel.Language,
                    Reference = objAdmissionModel.Reference,
                    Status = objAdmissionModel.Status,

                };
                objAdmissionDTO = await BALFactory.Instance.AdmissionBAL.UpdateAdmission(objAdmissionDTO);

                if (objAdmissionDTO.Done)
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
        [Route("AdmissionDetail")]
        public async Task<IActionResult> AdmissionDetail(long admissionid1)
        {
            try
            {
                AdmissionDTO objAdmissionDTO = await BALFactory.Instance.AdmissionBAL.GetAdmissionByID(admissionid1);
                AdmissionModel objAdmissionModel = new AdmissionModel()
                {
                    AdmissionID = objAdmissionDTO.AdmissionID,
                    Name = objAdmissionDTO.Name,
                    Email = objAdmissionDTO.Email,
                    DOB = objAdmissionDTO.DOB,
                    Gender = objAdmissionDTO.Gender,
                    Disability = objAdmissionDTO.Disability > 1 ? true : false,
                    Address = objAdmissionDTO.Address,
                    Mobile = objAdmissionDTO.Mobile,
                    AlternateMobile = objAdmissionDTO.AlternateMobile,
                    Previous_Institute = objAdmissionDTO.Previous_Institute,
                    Year_PassedOut = objAdmissionDTO.Year_PassedOut,
                    Score = objAdmissionDTO.Score,
                    Desired_Program = objAdmissionDTO.Desired_Program,
                    Language = objAdmissionDTO.Language,
                    Reference = objAdmissionDTO.Reference,
                    Status = objAdmissionDTO.Status,
                };
                if (objAdmissionModel != null)
                {
                    return PartialView("_AdmissionDetail", objAdmissionModel);
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
        [Route("EditSuccessMessage")]
        public async Task<IActionResult> EditSuccessMessage()
        {
            return View("_EditSuccessMessage");
        }


        [HttpGet]
        [Route("GetAllAdmission")]
        public async Task<IActionResult> GetAllAdmission()
        {
            try
            {
                List<AdmissionDTO> lstAdmissionDTO = await BALFactory.Instance.AdmissionBAL.GetAllAdmission();
                if(lstAdmissionDTO != null)
                {
                    return View(lstAdmissionDTO);
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
