namespace GISParser.Models.Binding
{
	#region Library Imports

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.IO;
	using Enumerators;
	using Helpers;

	#endregion

	[Table("ShapeFile")]
	public class ShapeFile : ImportFile
	{
		public ShapeFile()
		{
			Shapes = new HashSet<Shape>();
		}

		public ShapeFile(FileInfo file, int? srid)
			: base(file)
		{
			Shapes = new HashSet<Shape>();

			if (!file.Exists || !Exists)
				return;

			if (srid == null)
				throw new ArgumentNullException(
					"No SRID was provided. Unable to determine the spatial coordinate system to utilize for the shape file!",
					nameof(srid));

			SRID = (int)srid;

			ImportFromFile(file);
		}

		public double XMin { get; set; }

		public double YMin { get; set; }

		public double XMax { get; set; }

		public double YMax { get; set; }

		public double? ZMin { get; set; }

		public double? ZMax { get; set; }

		public double? MMin { get; set; }

		public double? MMax { get; set; }

		public int? SRID { get; set; }

		public ICollection<Shape> Shapes { get; set; }

		public void ImportFromFile(FileInfo file)
		{
			try
			{
				using (BinaryReader br = new BinaryReader(file.OpenRead()))
				{
					long streamLength = br.BaseStream.Length;
					Console.Write(StringHelper.GetProgressString(br.BaseStream.Position, streamLength, file.Name));
					FileCode = NumericsHelper.ReverseInt(br.ReadInt32());

					for (int i = 0; i < 5; i++)
						br.ReadInt32(); // Skip 5 empty Integer (4-byte) slots

					ContentLength = NumericsHelper.ReverseInt(br.ReadInt32()); // Big Endian, Reverse for actual value
					FileVersion = br.ReadInt32();
					ShapeType = (ShapeType)br.ReadInt32();
					XMin = br.ReadDouble();
					YMin = br.ReadDouble();
					XMax = br.ReadDouble();
					YMax = br.ReadDouble();
					ZMin = br.ReadDouble();
					ZMax = br.ReadDouble();
					MMin = br.ReadDouble();
					MMax = br.ReadDouble();

					//int rowsAffected;
					//using (ShapefileEntities db = new ShapefileEntities())
					//{
					//	db.Entry(this).State = EntityState.Added;
					//	rowsAffected = db.SaveChanges();
					//}

					//if (rowsAffected > 0
					//	&& Id > 0)
					//{
					//List<Shape> shapes = new List<Shape>();
					while (br.PeekChar() > -1)
					{
						Console.Write(StringHelper.GetProgressString(br.BaseStream.Position, streamLength, file.Name));
						Shapes.Add(new Shape(SRID, ShapeType, br));
					}

					Console.Write(StringHelper.GetProgressString(br.BaseStream.Position, streamLength, file.Name));

					//	using (SqlBulkCopy sbc = new SqlBulkCopy(DataHelper.DefaultConnectionString))
					//	{
					//		sbc.BatchSize = DataHelper.DefaultBatchSize;
					//		sbc.BulkCopyTimeout = DataHelper.DefaultTimeoutSeconds;
					//		sbc.DestinationTableName = "Shape";
					//		sbc.EnableStreaming = true;
					//		sbc.SqlRowsCopied += DataHelper.SqlBulkCopy_SqlRowsCopied;
					//		sbc.NotifyAfter = 250;

					//		sbc.ColumnMappings.Add("ShapeFileId", "ShapeFileId");
					//		sbc.ColumnMappings.Add("ShapeType", "ShapeType");
					//		sbc.ColumnMappings.Add("RecordNumber", "RecordNumber");
					//		sbc.ColumnMappings.Add("ContentLength", "ContentLength");
					//		sbc.ColumnMappings.Add("XMin", "XMin");
					//		sbc.ColumnMappings.Add("YMin", "YMin");
					//		sbc.ColumnMappings.Add("XMax", "XMax");
					//		sbc.ColumnMappings.Add("YMax", "YMax");
					//		sbc.ColumnMappings.Add("ZMin", "ZMin");
					//		sbc.ColumnMappings.Add("ZMax", "ZMax");
					//		sbc.ColumnMappings.Add("MMin", "MMin");
					//		sbc.ColumnMappings.Add("MMax", "MMax");
					//		sbc.ColumnMappings.Add("NumberOfParts", "NumberOfParts");
					//		sbc.ColumnMappings.Add("NumberOfPoints", "NumberOfPoints");
					//		sbc.ColumnMappings.Add("DTGeography", "Geography");

					//		try
					//		{
					//			DataTable shapesData = DataHelper.CreateDataTable(shapes);
					//			sbc.WriteToServerAsync(shapesData);
					//		}
					//		catch (Exception e)
					//		{
					//			Console.WriteLine($"\r\n{e.Message}\r\n{e}");
					//			throw;
					//		}
					//		finally
					//		{
					//			sbc.Close();
					//		}
					//	}
					//}
					//else
					//	throw new FileLoadException("The ShapeFile record failed to save properly or doesn't have a valid ID");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"\r\n{e.Message}\r\n{e}");
				throw;
			}
		}
	}
}