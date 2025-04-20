using ApiGateway.Authentication;
using ApiGateway.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Commands.AuthenticateUser;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AuthController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<AuthenticateUserResponse>),
            StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),
            StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse),
            StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest request,
            CancellationToken token)
        {
            var validator = new AuthenticateUserRequestValidator();
            var validationResult = await validator.ValidateAsync(request, token);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<AuthenticateUserCommand>(request);
            var response = await _mediator.Send(command, token);

            return Ok(new ApiResponseWithData<AuthenticateUserResponse>
            {
                Success = true,
                Message = "User authenticated successfully!",
                Data = _mapper.Map<AuthenticateUserResponse>(response)
            });
        }
    }
}
