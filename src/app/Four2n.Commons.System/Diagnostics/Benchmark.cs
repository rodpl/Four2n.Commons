// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Benchmark.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the Benchmark type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Diagnostics
{
    using global::System;
    using global::System.Diagnostics;

    /// <summary>
    /// Benchmark helper class for measuring performance.
    /// </summary>
    public class Benchmark : IDisposable
    {
        private readonly MeasureUnitType measureUnitType;
        private readonly Stopwatch timer = new Stopwatch();
        private OutputType currentOutputType;
        private string currentMessageFormat;
        private Action<object> outputDelegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Benchmark"/> class.
        /// </summary>
        /// <param name="measureUnitType">Type of the measure.</param>
        private Benchmark(MeasureUnitType measureUnitType)
        {
            this.measureUnitType = measureUnitType;
            this.timer.Start();
        }

        /// <summary>
        /// Definition of measure unit types.
        /// </summary>
        private enum MeasureUnitType
        {
            /// <summary>
            /// Miliseconds unit.
            /// </summary>
            Miliseconds
        }

        /// <summary>
        /// Definition of measured output types.
        /// </summary>
        private enum OutputType
        {
            /// <summary>
            /// Console output.
            /// </summary>
            Console,
            
            /// <summary>
            /// Delegate output.
            /// </summary>
            Delegate
        }

        /// <summary>
        /// Creates <see cref="Benchmark"/> measuring in miliseconds.
        /// </summary>
        /// <returns><see cref="Benchmark"/> instance for chaining.</returns>
        public static Benchmark InMiliseconds()
        {
            return new Benchmark(MeasureUnitType.Miliseconds);
        }

        /// <summary>
        /// Send benchmark output to the console..
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <returns><see cref="Benchmark"/> instance for chaining.</returns>
        public Benchmark ToConsole(string messageFormat)
        {
            this.currentMessageFormat = messageFormat;
            this.currentOutputType = OutputType.Console;
            this.timer.Reset();
            this.timer.Start();
            return this;
        }

        /// <summary>
        /// Send benchmark output to the delegate.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns><see cref="Benchmark"/> instance for chaining.</returns>
        public Benchmark ToDelegate(Action<object> action)
        {
            this.currentOutputType = OutputType.Delegate;
            this.outputDelegate = action;
            this.timer.Reset();
            this.timer.Start();
            return this;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.timer.Stop();
            object duration = null;

            switch (this.measureUnitType)
            {
                case MeasureUnitType.Miliseconds:
                    duration = this.timer.ElapsedMilliseconds;
                    break;
            }

            switch (this.currentOutputType)
            {
                case OutputType.Console:
                    Console.Out.WriteLine(this.currentMessageFormat, duration);
                    break;
                    
                case OutputType.Delegate:
                    this.outputDelegate(duration);
                    break;
            }
        }
    }
}