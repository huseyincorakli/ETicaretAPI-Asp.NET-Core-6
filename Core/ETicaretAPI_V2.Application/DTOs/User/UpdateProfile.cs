﻿namespace ETicaretAPI_V2.Application.DTOs.User
{
	public class UpdateProfile
	{
		public string NameSurname { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
	}
}