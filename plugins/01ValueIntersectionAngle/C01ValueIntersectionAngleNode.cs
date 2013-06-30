#region usings
using System;
using System.ComponentModel.Composition;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using VVVV.Core.Logging;
#endregion usings

namespace VVVV.Nodes
{
	#region PluginInfo
	[PluginInfo(Name = "Intersection Angle", Category = "Value", Help = "Basic template with one value in/out", Tags = "Angle, Intersection")]
	#endregion PluginInfo
	public class ValueAngularIntersectionNode : IPluginEvaluate
	{
		#region fields & pins
		[Input("Input Angle", DefaultValue = 1.0)]
		public ISpread<double> FInputAngle;
		
		[Input("From Angle", DefaultValue = 1.0)]
		public ISpread<double> FInputAngleFrom;
		
		[Input("To Angle", DefaultValue = 1.0)]
		public ISpread<double> FInputAngleTo;

		[Output("Output")]
		public ISpread<bool> FOutput;

		[Import()]
		public ILogger FLogger;
		#endregion fields & pins

		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{
			FOutput.SliceCount = SpreadMax;

			for (int i = 0; i < SpreadMax; i++)
			{
				FInputAngleFrom[i] = FInputAngleFrom[i] - Math.Truncate(FInputAngleFrom[i]);
				FInputAngleTo[i] = FInputAngleTo[i] - Math.Truncate(FInputAngleTo[i]);

				FInputAngleFrom[i] = FInputAngleFrom[i] - FInputAngle[i];
				if (FInputAngleFrom[i] > 0.5)
				FInputAngleFrom[i] = FInputAngleFrom[i] - 1;
				else if (FInputAngleFrom[i] <= -0.5)
				FInputAngleFrom[i] = FInputAngleFrom[i] + 1;
				

				FInputAngleTo[i] = FInputAngleTo[i] - FInputAngle[i];
				if (FInputAngleTo[i] > 0.5)
				FInputAngleTo[i] = FInputAngleTo[i] - 1;
				else if (FInputAngleTo[i] <= -0.5)
				FInputAngleTo[i] = FInputAngleTo[i] + 1;
				
				FOutput[i] = (Math.Sign(FInputAngleFrom[i]) != Math.Sign(FInputAngleTo[i])) && ((Math.Abs(FInputAngleFrom[i]) + Math.Abs(FInputAngleTo[i])) <= 0.5) | FInputAngleFrom[i]==0| FInputAngleTo[i]==0;
			}
			//FLogger.Log(LogType.Debug, "hi tty!");
		}
		
		
	}
}
