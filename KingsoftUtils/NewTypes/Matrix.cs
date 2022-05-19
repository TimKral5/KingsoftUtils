namespace Kingsoft.Utils.NewTypes
{
	public struct Matrix<MType>
	{
		public Matrix(MType[,] _matrix)
		{
			matrix = _matrix;
		}

		private MType[,] matrix { get; set; }

		public MType[,] InnerMatrix { get => matrix; }

		public (int, int) Size() => (matrix.GetLength(0), matrix.GetLength(1));

		public MType this[int x, int y] { get => matrix[x, y]; set => matrix[x, y] = value; }
	}
}
