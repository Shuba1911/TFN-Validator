using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TFN_Validator.Server.Model;
using TFN_Validator.Server.Services;

namespace TFN_Validator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TFNValidator : ControllerBase
    {
        private readonly ITFNLinkedValidator _tfnLinkedValidator;
        private readonly ITFNValidatorAlgorithm _tfnValidatorAlgorithm;
        public TFNValidator(ITFNValidatorAlgorithm tfnValidatorAlgorithm, ITFNLinkedValidator tfnLinkedValidator)
        {
            _tfnValidatorAlgorithm = tfnValidatorAlgorithm;
            _tfnLinkedValidator = tfnLinkedValidator;
        }
        [HttpGet]
        public ActionResult Validate(string Tfn) {

            try
            {
                string tfn = Tfn.Replace(" ", "");
                int tfnLength = tfn.Length;

                if (!_tfnLinkedValidator.IsTFNLinked(tfn))
                {
                    if (tfnLength == 8)
                    {
                        var result = _tfnValidatorAlgorithm.EightDigitTFNValidator(tfn);
                        return Ok(result);
                    }
                    else if (tfnLength == 9)
                    {
                        var result = _tfnValidatorAlgorithm.NineDigitTFNValidator(tfn);
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("TFN should at be atleast 8 or 9 digits long");
                    }
                }
                else
                {
                    return BadRequest("Validation tool does not allow multiple attempts for similar values");
                }

            }
            catch (Exception ex) 
            {
                return BadRequest("System failed to validate TFN");
            
            }
            
        }
    }
}
