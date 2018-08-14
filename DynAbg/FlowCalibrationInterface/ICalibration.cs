
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace FlowCalibrationInterface
{

    /// <summary>
    /// Schnittstelle zum hydraulischen Abgleich
    /// </summary>
    public interface ICalibration
    {


        /// <summary>
        /// Diese Eigenschaft gibt an, ob der EnOcean-Empfänger initialisiert ist.
        /// </summary>
        /// 

        // Tester
        event PropertyChangedEventHandler PropertyChanged;
        Thread allSyncThread { get; set; }

        string valvedatapath { get; set; }
        
        string lastMessageData { get; set; }

        bool midnightrecalib { get; set; }

        bool manualSoll { get; set; }

        bool simplifyCheck { get; set; }

        string lastEnOceanID { get; set; }
        System.DateTime lastTime { get; set; }
        bool Initialized { get; }

        /// <summary>
        /// Sucht und initialisiert den EnOcean-Empfänger
        /// </summary>
        /// <exception cref="CalibrationException">
        /// <b><see cref="Error"/>-Codes</b><br><br>
        ///	<i>NO_TCM_FOUND:</i><br>
        ///		Der EnOcean-Empfänger ist nicht angeschlossen oder kann nicht angesprochen werden.<br>
        /// </exception>
        void init();

        /// <summary>
        /// Beendet die Kommunikation mit dem EnOcean-Empfänger und gibt die Ressourcen frei.
        /// </summary>
        void close();

        /// <summary>
        /// Erzeugt ein neues Objekt mit der Schnittstelle IValve anhand eines empfangenen Anlerntelegramms.
        /// Dieses Ventil wird intern automatisch aktiviert und muss beim Löschen deaktiviert werden.
        /// </summary>
        /// <param name="timeout">Die Zeit in Millisekunden in der nach einem Anlerntelegramm eines  Ventils gesucht wird.</param>
        /// <returns>
        /// Ein <see cref="IValve"/> Objekt mit der Schnittstelle IValve.
        /// </returns>
        /// <exception cref="CalibrationException">
        /// <b><see cref="Error"/>-Codes</b><br><br>
        ///	<i>NOT_INITIALIZED:</i><br>
        ///		Der EnOcean-Empfänger wurde nicht initialisiert.<br><br>
        ///	<i>NO_LEARN_MESSAGE:</i><br>
        ///		In der Angegebenen Zeit wurde kein Anlerntelegramm empfangen.<br><br>
        ///	<i>VALVE_ALREADY_LEARNED:</i><br>
        ///		Es wurde versucht ein Ventil anzulernen, welches bereits angelernt wurde.<br><br>
        ///	<i>NO_FREE_IDS:</i><br>
        ///		Es sind bereits 128 Ventile angelernt. Weitere Ventile können nicht angelernt werden.<br>
        /// </exception>

        IValve anlernen(int timeout);


        IModule anlernenModule(int timeout);

        /// <summary>
        /// Fragt die ID des EnOcean-Empfängers ab.
        /// </summary>
        /// <returns>
        /// Die ID des EnOcean-Empfängers.
        /// </returns>
        /// <exception cref="CalibrationException">
        /// <b><see cref="Error"/>-Codes</b><br><br>
        ///	<i>NOT_INITIALIZED:</i><br>
        ///		Der EnOcean-Empfänger wurde nicht initialisiert.<br>
        /// </exception>
        long getTCMID();

        /// <summary>
        /// Deaktiviert ein angelerntes Ventil.
        /// </summary>
        /// <param name="valve">Ein Objekt mit Schnittstelle IValve.</param>
        /// <exception cref="CalibrationException">
        /// <b><see cref="Error"/>-Codes</b><br><br>
        ///	<i>UNKNOWN_VALVE_OBJECT:</i><br>
        ///		Es wurde ein Ventilobjekt übergeben, welches nicht mit der Methode anlernen() der Schnittstelle ICalibration erzeugt wurde.<br>
        /// </exception>
        void deactivateValve(IValve valve);

        /// <summary>
        /// Öffnet das Ventil zu 100 %.
        /// </summary>
        /// <param name="valve">Ein Objekt mit Schnittstelle IValve.</param>
        /// <exception cref="CalibrationException">
        /// <b><see cref="Error"/>-Codes</b><br><br>
        ///	<i>UNKNOWN_VALVE_OBJECT:</i><br>
        ///		Es wurde ein Ventilobjekt übergeben, welches nicht mit der Methode anlernen() der Schnittstelle ICalibration erzeugt wurde.<br>
        /// </exception>
        void openValve(IValve valve);

        /// <summary>
        /// Schließt das Ventil
        /// </summary>
        /// <param name="valve">Ein Objekt mit Schnittstelle IValve.</param>
        /// <exception cref="CalibrationException">
        /// <b><see cref="Error"/>-Codes</b><br><br>
        ///	<i>UNKNOWN_VALVE_OBJECT:</i><br>
        ///		Es wurde ein Ventilobjekt übergeben, welches nicht mit der Methode anlernen() der Schnittstelle ICalibration erzeugt wurde.<br>
        /// </exception>
        void closeValve(IValve valve);

        /// <summary>
		/// Aendert die gewuenschte temperatur
		/// </summary>
		/// <param name="valve">Ein Objekt mit Schnittstelle IValve.</param>
		/// <exception cref="CalibrationException">
		/// <b><see cref="Error"/>-Codes</b><br><br>
		///	<i>UNKNOWN_VALVE_OBJECT:</i><br>
		///		Es wurde ein Ventilobjekt übergeben, welches nicht mit der Methode anlernen() der Schnittstelle ICalibration erzeugt wurde.<br>
		/// </exception>
		void changeDesiredValue(IValve valve, int des);

        /// <summary>
		/// Aendert die gewuenschte temperatur
		/// </summary>
		/// <param name="valve">Ein Objekt mit Schnittstelle IValve.</param>
		/// <exception cref="CalibrationException">
		/// <b><see cref="Error"/>-Codes</b><br><br>
		///	<i>UNKNOWN_VALVE_OBJECT:</i><br>
		///		Es wurde ein Ventilobjekt übergeben, welches nicht mit der Methode anlernen() der Schnittstelle ICalibration erzeugt wurde.<br>
		/// </exception>
		void changeTempMode(IValve valve, int des);

        /// <summary>
        /// Setzt den Hub in mm. Die Voreinstellung ist 4 mm.<br>
        /// Einstellbar sind Werte von 1,0 bis 4,0 mm.<br>
        /// Sollte der übergebene Wert nicht in den o.a. Rahmen passen, so wird er entsprechend auf-/abgerundet.
        /// </summary>
        /// <param name="valve">Ein Objekt mit Schnittstelle IValve.</param>
        /// <param name="travel">Der einzustellende Hub in mm.</param>
        /// <exception cref="CalibrationException">
        /// <b><see cref="Error"/>-Codes</b><br><br>
        ///	<i>UNKNOWN_VALVE_OBJECT:</i><br>
        ///		Es wurde ein Ventilobjekt übergeben, welches nicht mit der Methode anlernen() der Schnittstelle ICalibration erzeugt wurde.<br>
        /// </exception>
        void setValveTravel(IValve valve, double travel);

        // <summary>
        // just a test
        // </summary>
        void pubsendAnswer(IValve valve);

        /// <summary>
        /// Gibt eine Kopie der Liste mit den angelernten Ventilen zurück
        /// </summary>
        /// <returns></returns>
        List<IValve> getLearnedValves();
        void deactivateModule(IModule module);
    }
}
