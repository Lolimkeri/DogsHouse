using DogsHouse.Interfaces;
using DogsHouse.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogsHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogController : Controller
    {
        private readonly IDogService _dogService;

        public DogController(IDogService dogService)
        {
            _dogService = dogService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] GetDogsDTO getDogsDTO)
        {
            try
            {
                var dogRecords = _dogService.GetDogRecords(getDogsDTO.SortColumn, getDogsDTO.SortOrder, getDogsDTO.PageNumber, getDogsDTO.PageSize);

                return Ok(dogRecords);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var dogRecord = _dogService.GetDogRecord(id);

                if (dogRecord == null)
                {
                    return NotFound();
                }

                return Ok(dogRecord);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDogDTO createDogDTO)
        {
            try
            {
                _dogService.AddDogRecord(createDogDTO.Name, createDogDTO.Color, createDogDTO.TailLength, createDogDTO.Weight);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateDogDTO updateDogDTO)
        {
            try
            {
                _dogService.UpdateDogRecord(updateDogDTO.Id, updateDogDTO.Name, updateDogDTO.Color, updateDogDTO.TailLength, updateDogDTO.Weight);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _dogService.DeleteDogRecord(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}