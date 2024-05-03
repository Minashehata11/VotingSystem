namespace PL.VotingSystem.Controllers
{

   [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class AdminController : BaseController
    {
        #region ImageSettingAllowed
        private List<string> _allowedExtensions = new List<string>() { ".png", ".jpg" };
        private long _maxAllowedPosterSize = 1048576 * 2;
        #endregion
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _UnitOfWork;

        public AdminController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Home()
        {
            var data = _UnitOfWork.AdminRepository.GetHomeData();

            return Ok(data);
        }
        [HttpGet("ManageElection")]
        public async Task<ActionResult<List<ViewMangeElectionDto>>> ManageElection()
        {

            var data = await _UnitOfWork.AdminRepository.MangeElectionDtos();
            return Ok(data);
        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteElection(int categoryId)
        {
            if (categoryId == 0) return BadRequest("Id Required");

            var voting = await _UnitOfWork.VotingRepository.GetAllVotingByCategoryIdAsync(categoryId);
            var category = await _UnitOfWork.CategoryRepository.GetById(categoryId);

            try
            {
                if (voting.Count() > 0)
                    await _UnitOfWork.VotingRepository.DeleteAllAsync(voting);
                if (category == null)
                    return BadRequest("Not Found Category");
                _UnitOfWork.CategoryRepository.Delete(category);
                _UnitOfWork.Commit();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(voting);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            if (id == 0)
                return NotFound("Not Found Id");

            var category = await _UnitOfWork.CategoryRepository.GetByIdWithIncludeAsync(id);
            if (category == null) return NotFound("Not Found Category");
            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<List<AllCandidateDto>>> GetAllCandidates(string? name)
        {
            List<Candidate> candidates = new List<Candidate>();
            if (string.IsNullOrEmpty(name))
                candidates = await _UnitOfWork.candidateRepository.GetAllWithInclude();
            else
                candidates = await _UnitOfWork.candidateRepository.SearchUserByNameAsync(name);

            if (candidates == null) return NotFound("Not Found Candidates");
            var mappedCandidates = _mapper.Map<List<AllCandidateDto>>(candidates);
            return Ok(mappedCandidates);
        }


        [HttpPost]
        public async Task<ActionResult<Category>> AddNewElection([FromForm] CreateCategoryDto input)
        {
            if (ModelState.IsValid)
            {
                var existingCandidates = await _UnitOfWork.candidateRepository.GetByManyIdsAsync(input.CandidatesId);
                if (input.CategoryLogo == null)
                    return BadRequest("Image Required");

                if (!_allowedExtensions.Contains(Path.GetExtension(input.CategoryLogo.FileName).ToLower()))
                    return BadRequest("Not Allowed Exitrons");

                if (_maxAllowedPosterSize < input.CategoryLogo.Length)
                    return BadRequest("Big than 2m");
                using var dataStream = new MemoryStream();
                await input.CategoryLogo.CopyToAsync(dataStream);


                Category category = new Category
                {
                    CategoryLogo = dataStream.ToArray(),
                    DateOfEndVoting = input.DateOfEndVoting,
                    Name = input.Name,
                    Candidates = existingCandidates

                };
                _UnitOfWork.CategoryRepository.Add(category);

                _UnitOfWork.Commit();
                return Ok(category);
            }
            else
                return BadRequest("Enter All Input");
        }

        [HttpGet]
        public async Task<ActionResult<List<UsersDto>>> GetAllUsers(string? name)
        {
            List<Voter> users = new List<Voter>();
            if (string.IsNullOrEmpty(name))
                users = await _UnitOfWork.voterRepository.GetAllUsersWithIncludeAsync();
            else
                users = await _UnitOfWork.voterRepository.SearchUserByNameAsync(name);


            var mappedUser = _mapper.Map<List<UsersDto>>(users);
            return mappedUser;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsersDto>> DeleteUser(string id)
        {
            if (id == null) return BadRequest("Not Found Id");

            var user = await _UnitOfWork.voterRepository.GetByIdWithIncludeAsync(id);

            if (user == null) return BadRequest("No User Found");

            var appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null) return BadRequest("No User Found");
            var mappedUser = _mapper.Map<UsersDto>(user);

            try
            {
                await _userManager.DeleteAsync(appUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(mappedUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewDto>> GetUserById(string id)
        {
            if (id == null) return BadRequest("Id Required");

            string base64Image = "";
            var user = await _UnitOfWork.voterRepository.GetByIdWithIncludeAsync(id);
            if (user is null)
                return BadRequest("User not found");
            if (user.User.Image != null)
                base64Image = Convert.ToBase64String(user.User.Image);


            var mappedUser = _mapper.Map<UserViewDto>(user);
            if (user.User.Gender == Gender.Male)
                mappedUser.Gender = "Male";
            else
                mappedUser.Gender = "Female";
            mappedUser.DateOfBirth = user.User.DateOfBirth.ToString("yyyy-MM-dd");
            mappedUser.ImageProile = base64Image;
            return mappedUser;
        }
        [HttpGet]
        public async Task<ActionResult<List<AdminDto>>> ManageAdmin(string? name)
        {
            List<Admin> admins = new List<Admin>();
            if (string.IsNullOrEmpty(name))
                admins = await _UnitOfWork.AdminRepository.GetAllUsersWithIncludeAsync();
            else
                admins = await _UnitOfWork.AdminRepository.SearchUserByNameAsync(name);

            var mappedAdmin = _mapper.Map<List<AdminDto>>(admins);

            return Ok(mappedAdmin);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AdminDto>> DeleteAdmin(string id)
        {
            if (id == null) return BadRequest("Not Found Id");

            var admin = await _UnitOfWork.AdminRepository.GetByIdWithIncludeAsync(id);

            if (admin == null) return BadRequest("No User Found");

            var appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null) return BadRequest("No User Found");
            var mappedAdmin = _mapper.Map<AdminDto>(admin);

            try
            {
                await _userManager.DeleteAsync(appUser);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(mappedAdmin);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminViewDto>> GetAdminById(string id)
        {
            if (id == null) return BadRequest("Id Required");

            string base64Image = "";
            var admin = await _UnitOfWork.AdminRepository.GetByIdWithIncludeAsync(id);
            if (admin is null)
                return BadRequest("Not Found Admin");
            if (admin.User.Image != null)
                base64Image = Convert.ToBase64String(admin.User.Image);

            var mappedAdmin = _mapper.Map<AdminViewDto>(admin);
            if (admin.User.Gender == Gender.Male)
                mappedAdmin.Gender = "Male";
            else
                mappedAdmin.Gender = "Female";
            mappedAdmin.DateOfBirth = admin.User.DateOfBirth.ToString("yyyy-MM-dd");
            mappedAdmin.ImageProile = base64Image;
            return mappedAdmin;
        }

        [HttpPost]
        public async Task<ActionResult<CreateAdminDto>> CreateNewAdmin([FromForm] CreateAdminDto input)
        {
            if (ModelState.IsValid)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(input.ImageProfile.FileName).ToLower()))
                    return BadRequest("ONly JPG and Png Allowed");
                if (input.ImageProfile.Length > _maxAllowedPosterSize)
                    return BadRequest("Max Allowed is 2M");
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user != null)
                    return BadRequest($"Registered {input.Email} Already Token");

                using var dataStream = new MemoryStream();
                await input.ImageProfile.CopyToAsync(dataStream);
                var appUser = new ApplicationUser
                {
                    Email = input.Email,
                    City = input.City,
                    UserName = input.Name,
                    FirstName = input.Email.Split('@')[0],
                    DateOfBirth = input.DateOfBirth,
                    SSN = input.SSN,

                    Admin = new Admin
                    {
                    },

                };
                appUser.Image = dataStream.ToArray();


                var result = await _userManager.CreateAsync(appUser, input.Password);
                if (result.Succeeded)
                    return Ok(input);
                else
                    return BadRequest("Error Create");

            }
            else
                return BadRequest("Not Valid");


        }
        [HttpGet]
        public async Task<ActionResult< List<PostDtoView>>> GetAllPostsOfCandidates()
        {
            List<PostDtoView> postDtoViews = new List<PostDtoView>();
          var posts= await _UnitOfWork.PosterRepository.GetAllPostsWithIncludeAsync();
            if (posts.Any())
            {
                foreach (var post in posts)
                {
                    PostDtoView postDtoView = new PostDtoView
                    {
                        Image = Convert.ToBase64String(post.PostImage),
                        Decription = post.Decription,
                        FullName=post.Candidate.User.FullName,
                        PostId = post.PostId,
                        Qualfication = post.Candidate.Qulification
                    };
                    postDtoViews.Add(postDtoView);  
            }    
                return Ok(postDtoViews);
            }
            return BadRequest("Not Posts Found");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeleteCandidatePosts(int id)
        {
            if (id == 0) return BadRequest("Not Found Id");
            var post = await _UnitOfWork.PosterRepository.GetByIdAsycn(id);

            if (post == null)
                return BadRequest("Not Found Post");
            _UnitOfWork.PosterRepository.Delete(post);
            _UnitOfWork.Commit();
            return Ok(post);
        }
        [HttpPost]
        public async Task<ActionResult<CreateCandidateDto>> AddCandidate([FromForm] CreateCandidateDto input)
        {
            if (input == null) return BadRequest("NOT Created");
            if (!_allowedExtensions.Contains(Path.GetExtension(input.Image.FileName).ToLower()))
                return BadRequest("ONly JPG and Png Allowed");
            if (input.Image.Length > _maxAllowedPosterSize)
                return BadRequest("Max Allowed is 2M");
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user != null)
                return BadRequest($"Registered {input.Email} Already Token");

            using var dataStream = new MemoryStream();
            await input.Image.CopyToAsync(dataStream);

            ApplicationUser appUser = new ApplicationUser
            {
                City = input.City,
                DateOfBirth = input.DateOfBirth,
                Email = input.Email,
                UserName = input.Name,
                FirstName = input.Name,
                LastName = input.LastName,
                Gender = input.Gender,
                SSN = input.SSN,
                Image = dataStream.ToArray(),
                Candidate = new Candidate
                {
                    Graduate = input.Graduate,
                    Qulification = input.Qulification,
                    Jop = input.jop,

                }

            };
            var result = await _userManager.CreateAsync(appUser, input.Password);

            if (!result.Succeeded)
                return BadRequest("Error In Create");
            await _userManager.AddToRoleAsync(appUser, "Candidate");
            return Ok(input);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCandidate(string id)
        {
            if (id == null) return BadRequest("Id Required");
            var candidates = _UnitOfWork.candidateRepository.GetById(id);
            if (candidates == null) return BadRequest("Not Found");
            var appUser = await _userManager.FindByIdAsync(candidates.CandidateId);
            if (appUser == null) return BadRequest("Not Found");
            await _userManager.DeleteAsync(appUser);
            return Ok();

        }
    }

}