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
    public class RoleMenuConfiguration : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> entity)
        {
            try
            {
                //RoleMenuId
                entity.HasKey(e => e.RoleMenuId);
                entity.Property(e => e.RoleMenuId)
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

                //MenuId
                entity.Property(e => e.MenuId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //RoleId
                entity.Property(e => e.RoleId)
                    .HasColumnType("int")
                    .IsRequired(true);

                
            }
            catch (Exception) { throw; }
        }
    }
}
