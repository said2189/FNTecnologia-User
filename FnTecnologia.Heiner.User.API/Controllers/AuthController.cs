using Application.Commands;
using Application.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FnTecnologia.Heiner.PruebaTecnica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                    return BadRequest(new { message = result.Error });

                return Ok(new { id = result.Value, message = "Usuario registrado correctamente." });
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registrando el usuario");
                return StatusCode(500, new { message = "Error en el sistema, intentelo mas tarde." });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                    return Unauthorized(new { message = result.Error });

                return Ok(new { token = result.Value?.Token, role = result.Value?.Role });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el login");
                return StatusCode(500, new { message = "Error en el sistema, intentelo mas tarde." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetUserQuery(id));

                if (!result.IsSuccess)
                    return NotFound(new { message = result.Error });

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario");
                return StatusCode(500, new { message = "Error en el sistema, intentelo mas tarder." });
            }
        }
    }
}

