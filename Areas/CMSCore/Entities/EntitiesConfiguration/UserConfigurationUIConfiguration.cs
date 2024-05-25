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
    public class UserConfigurationUIConfiguration : IEntityTypeConfiguration<UserConfigurationUI>
    {
        public void Configure(EntityTypeBuilder<UserConfigurationUI> entity)
        {
            try
            {
                //UserConfigurationUIId
                entity.HasKey(e => e.UserConfigurationUIId);
                entity.Property(e => e.UserConfigurationUIId)
                    .ValueGeneratedOnAdd();

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
                    .IsRequired(true);

                //ValueAsText
                entity.Property(e => e.ValueAsText)
                    .HasColumnType("text")
                    .IsRequired(false);

                //UserId
                entity.Property(e => e.UserId)
                    .HasColumnType("int")
                    .IsRequired(true);

                
            }
            catch (Exception) { throw; }
        }
    }
}