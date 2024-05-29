using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EmptyProject.Areas.EmptyProject.Entities;

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

namespace EmptyProject.Areas.EmptyProject.EntitiesConfiguration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> entity)
        {
            try
            {
                //ClientId
                entity.HasKey(e => e.ClientId);
                entity.Property(e => e.ClientId)
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

                //Boolean
                entity.Property(e => e.Boolean)
                    .HasColumnType("tinyint")
                    .IsRequired(true);

                //DateTime
                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .IsRequired(true);

                //Decimal
                entity.Property(e => e.Decimal)
                    .HasColumnType("numeric(18, 2)")
                    .IsRequired(true);

                //Integer
                entity.Property(e => e.Integer)
                    .HasColumnType("int")
                    .IsRequired(true);

                //TextArea
                entity.Property(e => e.TextArea)
                    .HasColumnType("text")
                    .IsRequired(false);

                //TextBasic
                entity.Property(e => e.TextBasic)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(false);

                //TextEditor
                entity.Property(e => e.TextEditor)
                    .HasColumnType("text")
                    .IsRequired(false);

                //TextEmail
                entity.Property(e => e.TextEmail)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(false);

                //TextFile
                entity.Property(e => e.TextFile)
                    .HasColumnType("varchar(MAX)")
                    .IsRequired(false);

                //TextHexColour
                entity.Property(e => e.TextHexColour)
                    .HasColumnType("varchar(7)")
                    .IsRequired(false);

                //TextPassword
                entity.Property(e => e.TextPassword)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(false);

                //TextPhoneNumber
                entity.Property(e => e.TextPhoneNumber)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(false);

                //TextTag
                entity.Property(e => e.TextTag)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(false);

                //TextURL
                entity.Property(e => e.TextURL)
                    .HasColumnType("varchar(8000)")
                    .IsRequired(false);

                //ClientStatusId
                entity.Property(e => e.ClientStatusId)
                    .HasColumnType("int")
                    .IsRequired(true);

                //TimeSpan
                entity.Property(e => e.TimeSpan)
                    .HasColumnType("time(7)")
                    .IsRequired(true);

                
            }
            catch (Exception) { throw; }
        }
    }
}
