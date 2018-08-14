using System;

namespace FlowCalibrationInterface
{
	/// <summary>
	/// Diese Klasse beschreibt Fehler im Zusammenhang mit den Klassen und Methoden
	/// aus der Bibliothek FlowCalibration.dll
	/// </summary>
	public class CalibrationException : Exception
    {
		/// <summary>
		/// Diese Eigenschaft beschreibt den Fehlercode
		/// </summary>
		public Error error { get; private set; }

		/// <summary>
		/// Erzeugt eine neue Instanz dieser Klasse mit dem angegebenem Fehlercode
		/// </summary>
		/// <param name="error">Ein entsprechender Fehlercode.</param>
		public CalibrationException(Error error)
        {
            this.error = error;
        }

		/// <summary>
		/// Erzeugt eine neue Instanz dieser Klasse mit dem angegebenem Fehlercode und einer auslösenden Exception
		/// </summary>
		/// <param name="error">Ein entsprechender Fehlercode.</param>
		/// <param name="exception">Eine vorrausgegangene Exception.</param>
		public CalibrationException(Error error, Exception exception)
            : base(null, exception)
        {
            this.error = error;
        }

		/// <summary>
		/// Erzeugt eine neue Instanz dieser Klasse mit dem angegebenem Fehlercode und einer Fehlerbeschreibung
		/// </summary>
		/// <param name="error">Ein entsprechender Fehlercode.</param>
		/// <param name="message">Eine Fehlerbeschreibung.</param>
		public CalibrationException(Error error, String message)
            : base(message)
        {
            this.error = error;
        }
    }
}
