using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EmptyProject.Areas.BasicCore.Entities;

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

namespace EmptyProject.Areas.BasicCore.EntitiesConfiguration
{
    public class FailureConfiguration : IEntityTypeConfiguration<Failure>
    {
        public void Configure(EntityTypeBuilder<Failure> entity)
        {
            try
            {
                //FailureId
                entity.HasKey(e => e.FailureId);
                entity.Property(e => e.FailureId)
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

                //Message
                entity.Property(e => e.Message)
                    .HasColumnType("text")
                    .IsRequired(true);

                //EmergencyLevel
                entity.Property(e => e.EmergencyLevel)
                    .HasColumnType("int")
                    .IsRequired(true);

                //StackTrace
                entity.Property(e => e.StackTrace)
                    .HasColumnType("text")
                    .IsRequired(true);

                //Source
                entity.Property(e => e.Source)
                    .HasColumnType("text")
                    .IsRequired(true);

                //Comment
                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .IsRequired(true);

                
            }
            catch (Exception) { throw; }
        }
    }
}
