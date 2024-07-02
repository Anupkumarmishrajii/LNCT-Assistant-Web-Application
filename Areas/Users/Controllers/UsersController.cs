using BAL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using WebLNCTAssistant.Areas.Users.Models;

namespace WebLNCTAssistant.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("Users")]
    public class UsersController : Controller
    {


        [HttpGet]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser()
        {
            try
            {
                UsersModel objUsersModel = new UsersModel();
                return View(objUsersModel);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UsersModel objUsersModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    objUsersModel.IsShowValidation = true;
                    objUsersModel.ErrorMessage = "validate model";
                    return View(objUsersModel);

                }
                UsersDTO objUsersDTO = new UsersDTO()
                {
                    Name = objUsersModel.Name,
                    Email = objUsersModel.Email,
                    IsLNCTIan = objUsersModel.IsLNCTIan ? 1 : 0,                 
                    As_a = objUsersModel.As_a,
                    HigherEaducation = objUsersModel.HigherEaducation,
                    Mobile = objUsersModel.Mobile,
                    Status = "PENDING"
                };

                objUsersDTO = await BALFactory.Instance.usersBAL.InsertUser(objUsersDTO);

                if (objUsersDTO.Done)
                {
                  return  RedirectToAction("SuccessMessage");
                }
                else
                {
                    return RedirectToAction("FailureMessage");
                }
            }
            catch
            {
                return RedirectToAction("FailureMessage");
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<UsersDTO> lstUsersDTO = await BALFactory.Instance.usersBAL.GetAllUsers();
                if (lstUsersDTO != null)
                {
                    return View(lstUsersDTO);
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
        [Route("EditUsers")]
        public async Task<IActionResult> EditUsers( long userid1)
        {
            try
            {
                UsersDTO objUsersDTO = await BALFactory.Instance.usersBAL.GetUserByID(userid1);
                UsersModel objUsersModel = new UsersModel()
                {
                    UsersID = objUsersDTO.UsersID,
                    Name = objUsersDTO.Name,
                    Email = objUsersDTO.Email,
                    IsLNCTIan = objUsersDTO.IsLNCTIan > 1 ? false : true,
                    As_a = objUsersDTO.As_a,
                    HigherEaducation = objUsersDTO.HigherEaducation,
                    Mobile = objUsersDTO.Mobile,
                    Status = objUsersDTO.Status,
                };
                if(objUsersModel != null)
                {
                    return View(objUsersModel);
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
        [Route("EditUsers")]
        public async Task<IActionResult> EditUsers(UsersModel objUsersModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    objUsersModel.IsShowValidation = true;
                    objUsersModel.ErrorMessage = "validate model";
                    return View(objUsersModel);

                }
                UsersDTO objUsersDTO = new UsersDTO()
                {
                    UsersID = objUsersModel.UsersID,
                    Name = objUsersModel.Name,
                    Email = objUsersModel.Email,
                    IsLNCTIan = objUsersModel.IsLNCTIan ? 1 : 0,
                    As_a = objUsersModel.As_a,
                    HigherEaducation = objUsersModel.HigherEaducation,
                    Mobile = objUsersModel.Mobile,
                    Status = objUsersModel.Status,
                };
                objUsersDTO = await BALFactory.Instance.usersBAL.UpdateUser(objUsersDTO);
                if (objUsersDTO.Done)
                {
                    return RedirectToAction("EditSuccessMessage");
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
        [Route("EditSuccessMessage")]
        public async Task<IActionResult> EditSuccessMessage()
        {
            return View("_EditSuccessMessage");
        }

        [HttpGet]
        [Route("FailureMessage")]
        public async Task<IActionResult> FailureMessage()
        {
            return View("_FailureMessage");
        }
    }
}
