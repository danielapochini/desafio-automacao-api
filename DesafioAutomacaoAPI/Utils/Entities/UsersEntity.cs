using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DesafioAutomacaoAPI.Utils.Entities
{
    public class UsersEntity
    {
	public int Id {get;set;}
	public string Username {get;set;}
	public string Realname {get;set;}
	public string Email {get;set;}
	public string Password {get;set;}
	public byte Enabled {get;set;} 
	public byte ProtectedUser {get;set;} 
	public short AccessLevel {get;set;}
	public int LoginCount {get;set;}
	public short LostPasswordRequestCount {get;set;}
	public short FailedoginCount {get;set;}
	public string CookieString {get;set;}
	public int LastVisit {get;set;}
	public int DateCreated {get;set;}

    }
}
