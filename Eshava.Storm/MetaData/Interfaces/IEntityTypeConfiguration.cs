using Eshava.Storm.MetaData.Builders;

namespace Eshava.Storm.MetaData.Interfaces
{
	public interface IEntityTypeConfiguration<TEntity> where TEntity : class
	{
		void Configure(EntityTypeBuilder<TEntity> builder);
	}
}