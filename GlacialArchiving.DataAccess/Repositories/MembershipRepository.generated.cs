using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlacialArchiving.DataAccess.Base;
using GlacialArchiving.DataAccess.DAL;
using GlacialArchiving.DataAccess.Models;

namespace GlacialArchiving.DataAccess.Repositories
{
    public partial class MembershipRepository : GenericRepository<Membership>
    {
        public MembershipRepository(DataContext context)
            : base(context)
        {
        }
        
        /*public ClaimTypeEnum Authenticate(User u, ModelEnum model)
        {
            SettingRepository sRepo = new SettingRepository((DataContext)this.context);
            foreach (string email in sRepo.ErrorEmail.Split(','))
            {
                if (string.Compare(email, u.EmailAddress, true) == 0)
                    return ClaimTypeEnum.FullAccess;
            }
        
            List<Membership> memberships = this.Get(x => x.MemberId == u.Id, null, null).ToList<Membership>();
            if (memberships.Count == 0)
                return ClaimTypeEnum.None;

            ClaimTypeEnum access = ClaimTypeEnum.None;
            foreach (Membership membership in memberships)
            {
                foreach (GroupClaim claim in membership.Group.Claims.Where(c => c.ModelId == (int)model))
                {
                    if (claim.PermissionId == (int)ClaimTypeEnum.FullAccess)
                        return ClaimTypeEnum.FullAccess;
                    else if (claim.PermissionId == (int)ClaimTypeEnum.Readonly)
                        access = ClaimTypeEnum.Readonly;
                }
            }
            return access;
        }
        public List<Model> GetAccessibleModels(User u, ClaimTypeEnum[] claims)
        {
            SettingRepository sRepo = new SettingRepository((DataContext)this.context);
            foreach (string email in sRepo.ErrorEmail.Split(','))
            {
                if (string.Compare(email, u.EmailAddress, true) == 0)
                    return new ModelRepository((DataContext)this.context).All.ToList<Model>();
            }

            List<Membership> memberships = this.Get(x => x.MemberId == u.Id, null, null).ToList<Membership>();
            if (memberships.Count == 0)
                return new List<Model>();

            List<Model> models = new List<Model>();
            foreach (Membership membership in memberships)
            {
                foreach (GroupClaim claim in membership.Group.Claims)
                {
                    if (models.Contains(claim.Model))
                        continue;

                    if (claims.Contains((ClaimTypeEnum)claim.PermissionId))
                        models.Add(claim.Model);
                }
            }

            return models;
        }*/
    }
}