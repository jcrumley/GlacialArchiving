using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using TBADev.MVC.Tools;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;

namespace GlacialArchiving.DataAccess.Migrations
{
    public partial class Seeding
    {
        public void SeedBase(DataContext context)
        {
			try
			{
            	this.BeforeSeed(context);
 
	            #region Section 'Users'
				try
	            {             
	                context.Users.AddOrUpdate(new User { UserId = 1, NameFirst = "Web", NameLast = "Site", EmailAddress = "info@tbadev.com", Password = "password", IsActive = false, IsLockedOut = true, IsVerified = false });
	                context.Users.AddOrUpdate(new User { UserId = 2, NameFirst = "John", NameLast = "Crumley", EmailAddress = "john@glptrading.com", Password = "password", IsActive = true, IsLockedOut = false, IsVerified = true });
	            }
	            catch (Exception ex)
	            {
	                string subject = "Error with seeding function in users";
	                string message = Utilities.CreateMessage(ex);
	                Utilities.SendEmail("dunhamcd@gmail.com", subject, message);
	            }
	   			#endregion
 			
	            #region Section 'NavIcons'
				
				try
	        	{
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 1, Name = "Asterisk", CssClassName = "glyphicon glyphicon-asterisk" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 2, Name = "Plus", CssClassName = "glyphicon glyphicon-plus" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 3, Name = "Euro", CssClassName = "glyphicon glyphicon-euro" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 4, Name = "Minus", CssClassName = "glyphicon glyphicon-minus" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 5, Name = "Cloud", CssClassName = "glyphicon glyphicon-cloud" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 6, Name = "Envelope", CssClassName = "glyphicon glyphicon-envelope" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 7, Name = "Pencil", CssClassName = "glyphicon glyphicon-pencil" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 8, Name = "Glass", CssClassName = "glyphicon glyphicon-glass" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 9, Name = "Music", CssClassName = "glyphicon glyphicon-music" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 10, Name = "Search", CssClassName = "glyphicon glyphicon-search" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 11, Name = "Heart", CssClassName = "glyphicon glyphicon-heart" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 12, Name = "Star", CssClassName = "glyphicon glyphicon-star" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 13, Name = "Star Empty", CssClassName = "glyphicon glyphicon-star-empty" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 14, Name = "User", CssClassName = "glyphicon glyphicon-user" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 15, Name = "Film", CssClassName = "glyphicon glyphicon-film" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 16, Name = "TH Large", CssClassName = "glyphicon glyphicon-th-large" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 17, Name = "TH", CssClassName = "glyphicon glyphicon-th" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 18, Name = "TH List", CssClassName = "glyphicon glyphicon-th-list" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 19, Name = "Ok", CssClassName = "glyphicon glyphicon-ok" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 20, Name = "Remove", CssClassName = "glyphicon glyphicon-remove" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 21, Name = "Zoom In", CssClassName = "glyphicon glyphicon-zoom-in" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 22, Name = "Zoom Out", CssClassName = "glyphicon glyphicon-zoom-out" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 23, Name = "Off", CssClassName = "glyphicon glyphicon-off" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 24, Name = "Signal", CssClassName = "glyphicon glyphicon-signal" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 25, Name = "Cog", CssClassName = "glyphicon glyphicon-cog" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 26, Name = "Trash", CssClassName = "glyphicon glyphicon-trash" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 27, Name = "Home", CssClassName = "glyphicon glyphicon-home" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 28, Name = "File", CssClassName = "glyphicon glyphicon-file" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 29, Name = "Time", CssClassName = "glyphicon glyphicon-time" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 30, Name = "Road", CssClassName = "glyphicon glyphicon-road" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 31, Name = "Download Alt", CssClassName = "glyphicon glyphicon-download-alt" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 32, Name = "Download", CssClassName = "glyphicon glyphicon-download" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 33, Name = "Upload", CssClassName = "glyphicon glyphicon-upload" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 34, Name = "Inbox", CssClassName = "glyphicon glyphicon-inbox" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 35, Name = "Play Circle", CssClassName = "glyphicon glyphicon-play-circle" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 36, Name = "Repeat", CssClassName = "glyphicon glyphicon-repeat" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 37, Name = "Refresh", CssClassName = "glyphicon glyphicon-refresh" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 38, Name = "List Alt", CssClassName = "glyphicon glyphicon-list-alt" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 39, Name = "Lock", CssClassName = "glyphicon glyphicon-lock" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 40, Name = "Flag", CssClassName = "glyphicon glyphicon-flag" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 41, Name = "Headphones", CssClassName = "glyphicon glyphicon-headphones" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 42, Name = "Volume Off", CssClassName = "glyphicon glyphicon-volume-off" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 43, Name = "Volume Down", CssClassName = "glyphicon glyphicon-volume-down" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 44, Name = "Volume Up", CssClassName = "glyphicon glyphicon-volume-up" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 45, Name = "QRCode", CssClassName = "glyphicon glyphicon-qrcode" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 46, Name = "Barcode", CssClassName = "glyphicon glyphicon-barcode" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 47, Name = "Tag", CssClassName = "glyphicon glyphicon-tag" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 48, Name = "Tags", CssClassName = "glyphicon glyphicon-tags" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 49, Name = "Book", CssClassName = "glyphicon glyphicon-book" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 50, Name = "Bookmark", CssClassName = "glyphicon glyphicon-bookmark" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 51, Name = "Print", CssClassName = "glyphicon glyphicon-print" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 52, Name = "Camera", CssClassName = "glyphicon glyphicon-camera" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 53, Name = "Font", CssClassName = "glyphicon glyphicon-font" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 54, Name = "Bold", CssClassName = "glyphicon glyphicon-bold" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 55, Name = "Italic", CssClassName = "glyphicon glyphicon-italic" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 56, Name = "Text Height", CssClassName = "glyphicon glyphicon-text-height" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 57, Name = "Text Width", CssClassName = "glyphicon glyphicon-text-width" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 58, Name = "Align Left", CssClassName = "glyphicon glyphicon-align-left" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 59, Name = "Align Center", CssClassName = "glyphicon glyphicon-align-center" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 60, Name = "Align Right", CssClassName = "glyphicon glyphicon-align-right" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 61, Name = "Align Justify", CssClassName = "glyphicon glyphicon-align-justify" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 62, Name = "List", CssClassName = "glyphicon glyphicon-list" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 63, Name = "Indent Left", CssClassName = "glyphicon glyphicon-indent-left" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 64, Name = "Indent Right", CssClassName = "glyphicon glyphicon-indent-right" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 65, Name = "FaceTime", CssClassName = "glyphicon glyphicon-facetime-video" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 66, Name = "Picture", CssClassName = "glyphicon glyphicon-picture" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 67, Name = "Map Marker", CssClassName = "glyphicon glyphicon-map-marker" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 68, Name = "Adjust", CssClassName = "glyphicon glyphicon-adjust" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 69, Name = "Tint", CssClassName = "glyphicon glyphicon-tint" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 70, Name = "Edit", CssClassName = "glyphicon glyphicon-edit" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 71, Name = "Share", CssClassName = "glyphicon glyphicon-share" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 72, Name = "Check", CssClassName = "glyphicon glyphicon-check" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 73, Name = "Move", CssClassName = "glyphicon glyphicon-move" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 74, Name = "Step Backward", CssClassName = "glyphicon glyphicon-step-backward" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 75, Name = "Fast Backward", CssClassName = "glyphicon glyphicon-fast-backward" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 76, Name = "Backward", CssClassName = "glyphicon glyphicon-backward" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 77, Name = "Play", CssClassName = "glyphicon glyphicon-play" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 78, Name = "Pause", CssClassName = "glyphicon glyphicon-pause" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 79, Name = "Stop", CssClassName = "glyphicon glyphicon-stop" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 80, Name = "Forward", CssClassName = "glyphicon glyphicon-forward" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 81, Name = "Fast Forward", CssClassName = "glyphicon glyphicon-fast-forward" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 82, Name = "Step Forward", CssClassName = "glyphicon glyphicon-step-forward" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 83, Name = "Eject", CssClassName = "glyphicon glyphicon-eject" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 84, Name = "Chevron Left", CssClassName = "glyphicon glyphicon-chevron-left" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 85, Name = "Chevron Right", CssClassName = "glyphicon glyphicon-chevron-right" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 86, Name = "Plus Sign", CssClassName = "glyphicon glyphicon-plus-sign" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 87, Name = "Minus Sign", CssClassName = "glyphicon glyphicon-minus-sign" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 88, Name = "Remove Sign", CssClassName = "glyphicon glyphicon-remove-sign" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 89, Name = "Ok Sign", CssClassName = "glyphicon glyphicon-ok-sign" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 90, Name = "Question Sign", CssClassName = "glyphicon glyphicon-question-sign" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 91, Name = "Info Sign", CssClassName = "glyphicon glyphicon-info-sign" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 92, Name = "Screenshot", CssClassName = "glyphicon glyphicon-screenshot" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 93, Name = "Remove Circle", CssClassName = "glyphicon glyphicon-remove-circle" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 94, Name = "Ok Circle", CssClassName = "glyphicon glyphicon-ok-circle" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 95, Name = "Ban Circle", CssClassName = "glyphicon glyphicon-ban-circle" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 96, Name = "Arrow Left", CssClassName = "glyphicon glyphicon-arrow-left" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 97, Name = "Arrow Right", CssClassName = "glyphicon glyphicon-arrow-right" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 98, Name = "Arrow Up", CssClassName = "glyphicon glyphicon-arrow-up" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 99, Name = "Arrow Down", CssClassName = "glyphicon glyphicon-arrow-down" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 100, Name = "Share Alt", CssClassName = "glyphicon glyphicon-share-alt" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 101, Name = "Resize Full", CssClassName = "glyphicon glyphicon-resize-full" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 102, Name = "Resize Small", CssClassName = "glyphicon glyphicon-resize-small" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 103, Name = "Exclamation Sign", CssClassName = "glyphicon glyphicon-exclamation-sign" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 104, Name = "Gift", CssClassName = "glyphicon glyphicon-gift" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 105, Name = "Leaf", CssClassName = "glyphicon glyphicon-leaf" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 106, Name = "Fire", CssClassName = "glyphicon glyphicon-fire" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 107, Name = "Eye Open", CssClassName = "glyphicon glyphicon-eye-open" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 108, Name = "Eye Close", CssClassName = "glyphicon glyphicon-eye-close" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 109, Name = "Warning Sign", CssClassName = "glyphicon glyphicon-warning-sign" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 110, Name = "Plane", CssClassName = "glyphicon glyphicon-plane" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 111, Name = "Calendar", CssClassName = "glyphicon glyphicon-calendar" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 112, Name = "Random", CssClassName = "glyphicon glyphicon-random" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 113, Name = "Comment", CssClassName = "glyphicon glyphicon-comment" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 114, Name = "Magnet", CssClassName = "glyphicon glyphicon-magnet" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 115, Name = "Chevron Up", CssClassName = "glyphicon glyphicon-chevron-up" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 116, Name = "Chevron Down", CssClassName = "glyphicon glyphicon-chevron-down" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 117, Name = "Retweet", CssClassName = "glyphicon glyphicon-retweet" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 118, Name = "Shopping Cart", CssClassName = "glyphicon glyphicon-shopping-cart" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 119, Name = "Folder Close", CssClassName = "glyphicon glyphicon-folder-close" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 120, Name = "Folder Open", CssClassName = "glyphicon glyphicon-folder-open" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 121, Name = "Resize Vertical", CssClassName = "glyphicon glyphicon-resize-vertical" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 122, Name = "Resize Horizontal", CssClassName = "glyphicon glyphicon-resize-horizontal" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 123, Name = "HDD", CssClassName = "glyphicon glyphicon-hdd" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 124, Name = "Bullhorn", CssClassName = "glyphicon glyphicon-bullhorn" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 125, Name = "Bell", CssClassName = "glyphicon glyphicon-bell" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 126, Name = "Certificate", CssClassName = "glyphicon glyphicon-certificate" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 127, Name = "Thumbs Up", CssClassName = "glyphicon glyphicon-thumbs-up" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 128, Name = "Thumbs Down", CssClassName = "glyphicon glyphicon-thumbs-down" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 129, Name = "Hand Right", CssClassName = "glyphicon glyphicon-hand-right" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 130, Name = "Hand Left", CssClassName = "glyphicon glyphicon-hand-left" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 131, Name = "Hand Up", CssClassName = "glyphicon glyphicon-hand-up" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 132, Name = "Hand Down", CssClassName = "glyphicon glyphicon-hand-down" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 133, Name = "Circle Arrow Right", CssClassName = "glyphicon glyphicon-circle-arrow-right" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 134, Name = "Circle Arrow Left", CssClassName = "glyphicon glyphicon-circle-arrow-left" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 135, Name = "Circle Arrow Up", CssClassName = "glyphicon glyphicon-circle-arrow-up" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 136, Name = "Circle Arrow Down", CssClassName = "glyphicon glyphicon-circle-arrow-down" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 137, Name = "Globe", CssClassName = "glyphicon glyphicon-globe" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 138, Name = "Wrench", CssClassName = "glyphicon glyphicon-wrench" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 139, Name = "Tasks", CssClassName = "glyphicon glyphicon-tasks" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 140, Name = "Filter", CssClassName = "glyphicon glyphicon-filter" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 141, Name = "Briefcase", CssClassName = "glyphicon glyphicon-briefcase" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 142, Name = "Fullscreen", CssClassName = "glyphicon glyphicon-fullscreen" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 143, Name = "Dashboard", CssClassName = "glyphicon glyphicon-dashboard" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 144, Name = "Paperclip", CssClassName = "glyphicon glyphicon-paperclip" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 145, Name = "Heart Empty", CssClassName = "glyphicon glyphicon-heart-empty" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 146, Name = "Link", CssClassName = "glyphicon glyphicon-link" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 147, Name = "Phone", CssClassName = "glyphicon glyphicon-phone" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 148, Name = "Pushpin", CssClassName = "glyphicon glyphicon-pushpin" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 149, Name = "USD", CssClassName = "glyphicon glyphicon-usd" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 150, Name = "GBP", CssClassName = "glyphicon glyphicon-gbp" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 151, Name = "Sort", CssClassName = "glyphicon glyphicon-sort" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 152, Name = "Sort By Alphabet", CssClassName = "glyphicon glyphicon-sort-by-alphabet" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 153, Name = "Sort By Alphabet Alt", CssClassName = "glyphicon glyphicon-sort-by-alphabet-alt" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 154, Name = "Sort By Order", CssClassName = "glyphicon glyphicon-sort-by-order" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 155, Name = "Sort By Order Alt", CssClassName = "glyphicon glyphicon-sort-by-order-alt" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 156, Name = "Sort By Attributes", CssClassName = "glyphicon glyphicon-sort-by-attributes" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 157, Name = "Sort By Attributes Alt", CssClassName = "glyphicon glyphicon-sort-by-attributes-alt" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 158, Name = "Unchecked", CssClassName = "glyphicon glyphicon-unchecked" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 159, Name = "Expand", CssClassName = "glyphicon glyphicon-expand" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 160, Name = "Collapse Down", CssClassName = "glyphicon glyphicon-collapse-down" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 161, Name = "Collapse Up", CssClassName = "glyphicon glyphicon-collapse-up" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 162, Name = "Log In", CssClassName = "glyphicon glyphicon-log-in" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 163, Name = "Flash", CssClassName = "glyphicon glyphicon-flash" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 164, Name = "Log Out", CssClassName = "glyphicon glyphicon-log-out" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 165, Name = "New Window", CssClassName = "glyphicon glyphicon-new-window" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 166, Name = "Record", CssClassName = "glyphicon glyphicon-record" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 167, Name = "Save", CssClassName = "glyphicon glyphicon-save" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 168, Name = "Open", CssClassName = "glyphicon glyphicon-open" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 169, Name = "Saved", CssClassName = "glyphicon glyphicon-saved" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 170, Name = "Import", CssClassName = "glyphicon glyphicon-import" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 171, Name = "Export", CssClassName = "glyphicon glyphicon-export" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 172, Name = "Send", CssClassName = "glyphicon glyphicon-send" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 173, Name = "Floppy Disk", CssClassName = "glyphicon glyphicon-floppy-disk" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 174, Name = "Floppy Saved", CssClassName = "glyphicon glyphicon-floppy-saved" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 175, Name = "Floppy Remove", CssClassName = "glyphicon glyphicon-floppy-remove" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 176, Name = "Floppy Save", CssClassName = "glyphicon glyphicon-floppy-save" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 177, Name = "Floppy Open", CssClassName = "glyphicon glyphicon-floppy-open" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 178, Name = "Credit Card", CssClassName = "glyphicon glyphicon-credit-card" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 179, Name = "Transfer", CssClassName = "glyphicon glyphicon-transfer" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 180, Name = "Cutlery", CssClassName = "glyphicon glyphicon-cutlery" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 181, Name = "Header", CssClassName = "glyphicon glyphicon-header" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 182, Name = "Compressed", CssClassName = "glyphicon glyphicon-compressed" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 183, Name = "Earphone", CssClassName = "glyphicon glyphicon-earphone" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 184, Name = "Phone Alt", CssClassName = "glyphicon glyphicon-phone-alt" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 185, Name = "Tower", CssClassName = "glyphicon glyphicon-tower" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 186, Name = "Stats", CssClassName = "glyphicon glyphicon-stats" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 187, Name = "SD Video", CssClassName = "glyphicon glyphicon-sd-video" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 188, Name = "HD Video", CssClassName = "glyphicon glyphicon-hd-video" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 189, Name = "Subtitles", CssClassName = "glyphicon glyphicon-subtitles" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 190, Name = "Sound Stereo", CssClassName = "glyphicon glyphicon-sound-stereo" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 191, Name = "Sound Dolby", CssClassName = "glyphicon glyphicon-sound-dolby" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 192, Name = "Sound 5-1", CssClassName = "glyphicon glyphicon-sound-5-1" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 193, Name = "Sound 6-1", CssClassName = "glyphicon glyphicon-sound-6-1" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 194, Name = "Sound 7-1", CssClassName = "glyphicon glyphicon-sound-7-1" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 195, Name = "Copyright Mark", CssClassName = "glyphicon glyphicon-copyright-mark" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 196, Name = "Registration Mark", CssClassName = "glyphicon glyphicon-registration-mark" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 197, Name = "Cloud Download", CssClassName = "glyphicon glyphicon-cloud-download" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 198, Name = "Cloud Upload", CssClassName = "glyphicon glyphicon-cloud-upload" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 199, Name = "Tree Conifer", CssClassName = "glyphicon glyphicon-tree-conifer" });
                	context.NavIcons.AddOrUpdate(new NavIcon { NavIconId = 200, Name = "Tree Deciduous", CssClassName = "glyphicon glyphicon-tree-deciduous" });
		 		}
	            catch (Exception ex)
	            {
	                string subject = "Error with seeding function in NavIcons";
	                string message = Utilities.CreateMessage(ex);
	                Utilities.SendEmail("dunhamcd@gmail.com", subject, message);
	            }
		
	            #endregion
            
	            #region Section 'Settings'
					
				try
	            {
                	context.Settings.AddOrUpdate(new Setting { SettingId = 1, Name = "ErrorEmail", Data = @"john.crumley@gmail.com", Type = "string", IsStatic = true });
                	context.Settings.AddOrUpdate(new Setting { SettingId = 2, Name = "WebUserId", Data = @"1", Type = "int", IsStatic = true });
	            }
		        catch (Exception ex)
		        {
	                string subject = "Error with seeding function in Settings";
	                string message = Utilities.CreateMessage(ex);
	                Utilities.SendEmail("dunhamcd@gmail.com", subject, message);
	            }
				#endregion
            
	            #region Section 'UploadedItemStatuss'
				
				try
	        	{
                	context.UploadedItemStatuss.AddOrUpdate(new UploadedItemStatus { UploadedItemStatusId = 1, Name = "Validated" });
                	context.UploadedItemStatuss.AddOrUpdate(new UploadedItemStatus { UploadedItemStatusId = 2, Name = "Partially Stored" });
                	context.UploadedItemStatuss.AddOrUpdate(new UploadedItemStatus { UploadedItemStatusId = 3, Name = "Fully Stored" });
		 		}
	            catch (Exception ex)
	            {
	                string subject = "Error with seeding function in UploadedItemStatuss";
	                string message = Utilities.CreateMessage(ex);
	                Utilities.SendEmail("dunhamcd@gmail.com", subject, message);
	            }
		
	            #endregion
            
	            #region Section 'Navigation'
				try
				{
					//top level navigations
	                context.Navigations.AddOrUpdate(new Navigation { NavigationId = 1, Name = "Default", SystemGenerated = true });
				
					//schema specific nav items
					context.NavItems.AddOrUpdate(x => new { x.Title, x.URL, x.SystemGenerated }, new NavItem {
						NavItemId = 1000, TopLevelId = 1, 
						Title = "Admin - dbo", URL = "/Dashboard", Sequence = 11000, SystemGenerated = true });
					
					context.NavItems.AddOrUpdate(x => new { x.Title, x.URL, x.SystemGenerated }, new NavItem {
						NavItemId = 1001, TopLevelId = 1, 
						Title = "Admin - datastorage", URL = "/Dashboard", Sequence = 11010, SystemGenerated = true });
					
					context.NavItems.AddOrUpdate(x => new { x.Title, x.URL, x.SystemGenerated }, new NavItem {
						NavItemId = 1002, TopLevelId = 1, 
						Title = "Admin - enum", URL = "/Dashboard", Sequence = 11020, SystemGenerated = true });
					
					context.NavItems.AddOrUpdate(x => new { x.Title, x.URL, x.SystemGenerated }, new NavItem {
						NavItemId = 1003, TopLevelId = 1, 
						Title = "Admin - audit", URL = "/Dashboard", Sequence = 11030, SystemGenerated = true });
					
					context.NavItems.AddOrUpdate(x => new { x.Title, x.URL, x.SystemGenerated }, new NavItem {
						NavItemId = 1004, TopLevelId = 1, 
						Title = "Admin - nav", URL = "/Dashboard", Sequence = 11040, SystemGenerated = true });
					
	                
	                //schema specific nav sub items
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=2, 
						NavItemId = 1000, Title = "Clients", URL = "/Admin/Clients", 
						Sequence = 1, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=3, 
						NavItemId = 1001, Title = "Customer Coverages", URL = "/Admin/CustomerCoverages", 
						Sequence = 2, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=4, 
						NavItemId = 1001, Title = "Data Tags", URL = "/Admin/DataTags", 
						Sequence = 3, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=5, 
						NavItemId = 1000, Title = "File Data", URL = "/Admin/FileData", 
						Sequence = 4, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=6, 
						NavItemId = 1002, Title = "File Types", URL = "/Admin/FileTypes", 
						Sequence = 5, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=7, 
						NavItemId = 1000, Title = "freds", URL = "/Admin/freds", 
						Sequence = 6, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=8, 
						NavItemId = 1001, Title = "GlacierStorages", URL = "/Admin/GlacierStorages", 
						Sequence = 7, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=9, 
						NavItemId = 1003, Title = "History Logs", URL = "/Admin/HistoryLogs", 
						Sequence = 8, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=10, 
						NavItemId = 1000, Title = "Memberships", URL = "/Admin/Memberships", 
						Sequence = 9, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=11, 
						NavItemId = 1004, Title = "Nav Icons", URL = "/Admin/NavIcons", 
						Sequence = 10, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=12, 
						NavItemId = 1004, Title = "Navigations", URL = "/Admin/Navigations", 
						Sequence = 11, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=13, 
						NavItemId = 1004, Title = "Nav Items", URL = "/Admin/NavItems", 
						Sequence = 12, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=14, 
						NavItemId = 1004, Title = "Nav Sub Items", URL = "/Admin/NavSubItems", 
						Sequence = 13, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=15, 
						NavItemId = 1001, Title = "Payload Types", URL = "/Admin/PayloadTypes", 
						Sequence = 14, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=16, 
						NavItemId = 1001, Title = "Replacements", URL = "/Admin/Replacements", 
						Sequence = 15, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=17, 
						NavItemId = 1000, Title = "Simple HTML Pages", URL = "/Admin/SimpleHTMLPages", 
						Sequence = 16, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=18, 
						NavItemId = 1000, Title = "StoredFile", URL = "/Admin/StoredFile", 
						Sequence = 17, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=19, 
						NavItemId = 1001, Title = "Trading Unit Coverages", URL = "/Admin/TradingUnitCoverages", 
						Sequence = 18, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=20, 
						NavItemId = 1001, Title = "Uploaded Items", URL = "/Admin/UploadedItems", 
						Sequence = 19, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=21, 
						NavItemId = 1001, Title = "UploadedItemStatuss", URL = "/Admin/UploadedItemStatuss", 
						Sequence = 20, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=22, 
						NavItemId = 1000, Title = "Uploaded Item Validations", URL = "/Admin/UploadedItemValidations", 
						Sequence = 21, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=23, 
						NavItemId = 1000, Title = "Users", URL = "/Admin/Users", 
						Sequence = 22, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=24, 
						NavItemId = 1000, Title = "User Groups", URL = "/Admin/UserGroups", 
						Sequence = 23, SystemGenerated = true });
					context.NavSubItems.AddOrUpdate((x => new { x.Title, x.URL, x.SystemGenerated }),
						new NavSubItem { NavSubItemId=25, 
						NavItemId = 1000, Title = "Validation Errors", URL = "/Admin/ValidationErrors", 
						Sequence = 24, SystemGenerated = true });
		
				}
	            catch (Exception ex)
	            {
	                string subject = "Error with seeding function in Navigation";
	                string message = Utilities.CreateMessage(ex);
	                Utilities.SendEmail("dunhamcd@gmail.com", subject, message);
	            }
	            #endregion
        
                context.SaveChanges();
                this.AfterSeed(context);
				
			}
            catch (Exception ex)
            {
                string subject = "Error with seeding function in Navigation";
                string message = Utilities.CreateMessage(ex);
                Utilities.SendEmail("dunhamcd@gmail.com", subject, message);
            }

        }
        partial void BeforeSeed(DataContext context); 
        partial void AfterSeed(DataContext context); 
    }
}