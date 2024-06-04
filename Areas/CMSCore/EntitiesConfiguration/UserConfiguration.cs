using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EmptyProject.Areas.CMSCore.Entities;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace EmptyProject.Areas.CMSCore.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            try
            {
                //UserId
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd();

                //Active
                entity.Property(e => e.Active)
                    .HasColumnType("tinyint")
                    .IsRequired(true);

                //DateTimeCreation
                entity.Property(e => e.DateTimeCreation)
                    .HasColumnType("datetime")
                    .IsRequired(true);

                //DateTimeLastModification
                entity.Property(e => e.DateTimeLastModification)
                    .HasColumnType("datetime")
                    .IsRequired(true);

                //UserCreationId
                entity.Property(e => e.UserCreationId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //UserLastModificationId
                entity.Property(e => e.UserLastModificationId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //Email
                entity.Property(e => e.Email)
                    .HasColumnType("varchar(400)")
                    .IsRequired(true);

                //Password
                entity.Property(e => e.Password)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(true);

                //RoleId
                entity.Property(e => e.RoleId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //ProfilePicture
                entity.Property(e => e.ProfilePicture)
                    .HasColumnType("varchar(MAX)")
                    .IsRequired(false);

                
            }
            catch (Exception) { throw; }
        }
    }
}
