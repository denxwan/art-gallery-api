using System.Windows.Input;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using art_gallery_api.Persistence;
using art_gallery_api.Models;

namespace art_gallery_api.Controllers;

[ApiController]
[Route("api/exhibitions")]
public class ExhibitionsController : ControllerBase
{
    // Dependency Injection

    private readonly IExhibitionDataAccess _exhibitionsRepo;
    public ExhibitionsController(IExhibitionDataAccess userRepo)
    {
        _exhibitionsRepo = userRepo;
    }

    // Endpoints here

    // -- SELECT COMMANDS --

    [HttpGet()]
    public IEnumerable<Exhibition> GetExhibitions()
    {
        return _exhibitionsRepo.GetExhibitions();
    }

    // [HttpGet("square")]
    // public IEnumerable<Map> GetSquareMapsOnly()
    // {
    //     return _mapRepo.GetSquareMapsOnly();
    // }

    [HttpGet("{id}", Name = "GetExhibitions")]
    public IActionResult GetExhibitionById(int id)
    {
        foreach (var exhibition in _exhibitionsRepo.GetExhibitions())
        {
            if (exhibition.ExhibitionId == id)
            {
                return Ok(exhibition);
            }
        }
        return NotFound();
    }

    // -- INSERT COMMANDS --

    [HttpPost()]
    public IActionResult AddExhibition(Exhibition newExhibition)
    {
        if(newExhibition == null)
        {
            return BadRequest();
        }

        foreach (var exhibition in _exhibitionsRepo.GetExhibitions())
        {
            if((newExhibition.ExhibitionTitle.Equals(exhibition.ExhibitionTitle)) && (newExhibition.ExhibitionDate.Equals(exhibition.ExhibitionDate)))
            {
                return Conflict();
            }
        }

        Exhibition result = _exhibitionsRepo.InsertExhibition(newExhibition);

        return Created("GetExhibition", result);
    }

    // -- UPDATE COMMANDS --

    [HttpPut("{id}")]
    public IActionResult UpdateEchibition(int id, Exhibition updatedExhibition)
    {
        foreach (var exhibition in _exhibitionsRepo.GetExhibitions())
        {
            if (exhibition.ExhibitionId == id)
            {
                Exhibition result;

                try
                {
                    result = _exhibitionsRepo.UpdateExhibition(exhibition, updatedExhibition);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    return BadRequest();
                }

                return Ok(result);
            }
        }
        return NotFound();
    }

    // -- DELETE COMMANDS --

    [HttpDelete("{id}")]
    public IActionResult DeleteExhibition(int id)
    {
        foreach (var exhibition in _exhibitionsRepo.GetExhibitions())
        {
            if (exhibition.ExhibitionId == id)
            {
                _exhibitionsRepo.RemoveExhibition(id);
                return Ok();
            }
        }
        return NotFound();
    }
}
