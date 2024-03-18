using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

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

namespace EmptyProject.Areas.CMSCore.Entities.EntitiesConfiguration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> entity)
        {
            try
            {
                //MenuId
                entity.HasKey(e => e.MenuId);
                entity.Property(e => e.MenuId)
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

                //Name
                entity.Property(e => e.Name)
                    .HasColumnType("varchar(200)")
                    .IsRequired(false);

                //MenuFatherId
                entity.Property(e => e.MenuFatherId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //Order
                entity.Property(e => e.Order)
                    .HasColumnType("int")
                    .IsRequired(true);

                //URLPath
                entity.Property(e => e.URLPath)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(false);

                //IconURLPath
                entity.Property(e => e.IconURLPath)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(false);

                
            }
            catch (Exception) { throw; }
        }
    }
}
