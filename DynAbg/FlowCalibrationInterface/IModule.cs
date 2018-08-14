using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FlowCalibrationInterface;

namespace FlowCalibrationInterface
{
    /// <summary>
    /// Diese Schnittstelle beschreibt die Eigenschaften eines Ventils.
    ///	Obwohl der Aktor, welches das Ventil antreibt und das Ventil selbst zwei
    ///	eigenständige Objekte sind, werden diese hier zusammengefasst und als Ventil behandelt.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Dieses Ereignis tritt bei Änderungen der Eigenschaften einer Instanz dieser Klasse auf.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Diese Eigenschaft beschreibt die EnOcean-ID des Ventils.
        /// </summary>
        /// 
        
        DateTime dateTimeReceived { get; set; }

        int type { get; set; }
        int FunctionID { get; set; }
        long ModuleID { get; }

        bool first { get; set; }

        bool incomplete { get; set; }

        bool datasend {get;set;}
        bool dataask { get; set; }

        bool configsend { get; set; }
        bool configask { get; set; }

        bool roomsend { get; set; }
        bool roomask { get; set; }

        bool timesend { get; set; }
        bool timeask { get; set; }
        bool timeask2 { get; set; }


        List<timeProgram> timeprog { get; set; }



        /// <summary>
        /// Diese Eigenschaft beschreibt die aktuell gemessene Temperatur.
        /// </summary>
        double Temperature { get; }


        /// <summary>
        /// Diese Eigenschaft beschreibt, ob die Kommunikation mit dem Ventil in Ordnung ist.
        /// Wenn das angelernte Ventil sich innerhalb von 7 Sekunden
        /// nicht gemeldet hat, wird dieses Flag auf <code>false</code> gesetzt.
        /// </summary>
        bool KommunikationOK { get; set; }

        /// <summary>
		/// Diese Eigenschaft beschreibt die Signalstärke in Dezibel(dB) von 0 bis -255 
		/// </summary>
		int Signal { get; }


        /// <summary>
        /// Diese Eigenschaft beschreibt, ob die Fenster offen ist.
        /// </summary>
        bool WindowOpen { get; set; }

        /// <summary>
        /// SetPoint
        /// </summary>
        double SetPoint { get; set; }

        /// <summary>
        /// EcoSetPoint
        /// </summary>
        double EcoSetPoint { get; set; }

        /// <summary>
        /// ComSetPoint
        /// </summary>
        double ComSetPoint { get; set; }

        /// <summary>
        /// Intervall zur Synchronisierung in Minuten 1-60, ansonsten 3, 12 oder 24 stunden.
        /// </summary>
        int Intervall { get; set; }

        /// <summary>
        ///	Diese Eigenschaft beschreibt den Hersteller des Ventils.
        ///	Diese Eigenschaft muss gesetzt werden, damit der hydraulische Abgleich gestartet werden kann.
        /// </summary>
        string Manufacturer { get; set; }
        List<timeProgram> timeprogsend { get; set; }
        bool NewTime { get; set; }
    }
}
