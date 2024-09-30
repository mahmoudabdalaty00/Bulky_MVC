﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models.Models
{
	public class ApplicationUser :IdentityUser 
	{
		[Required]
		public string Name { get; set; }

		public string? StreetAddress { get; set; }
		public string? City { get; set; }

		[DisplayName("State")]
		public string? State { get; set; }
		public string? PostalCode { get; set; }


		//conection between Customer & Company 
		public int? CompanyId { get; set; }
		[ForeignKey(nameof(CompanyId))]
		[ValidateNever]
		public Company Company { get; set; }
		 
	}
}
