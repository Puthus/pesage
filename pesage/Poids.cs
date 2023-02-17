using System.Windows.Forms;
using pesage.pesageDataSetTableAdapters;

namespace pesage
{
    public class Poids
    {
        private bool _isStable;
        private double _tare;
        private double _weight;
        private Label _label;
        private Label _tarlabel;
        public double Weight
        { get { return _weight; } set { _weight = value; _label.Text = ToString(); } }
        public double Tare
        { get { return _tare; } set { _tare = value; _label.Text = ToString(); _tarlabel.Text = $"{_tare:0.00} KG"; } }
        public bool IsStable
        {
            get { return _isStable; }
            set
            {
                _isStable = value;
                _label.Text = ToString();
                _label.ForeColor = value ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            }
        }
        public Label Label
        { get { return _label; } set { _label = value; _label.Text = ToString(); } }
        public Label TarLabel
        { get { return _tarlabel; } set { _tarlabel = value; _tarlabel.Text = $"{_tare:0.00} KG"; } }
        public Poids()
        {
            _isStable = false;
            _tare = 0;
            _weight = 0;
        }

        //toString
        public override string ToString()
        {
            return $@"{_weight - _tare:0.00} KG";
        }
    }

    public class CodeBarre
    {
        private int _client;
        private int _service;
        private int _residu;
        private int _conteneur;
        private int _operateur;
        private int _ticket;
        private Label _label;
        public int Client
        {
            get { return _client; }
            set
            {
                _client = value;
                Ticket = CalcTicketID();
            }
        }
        public int Service
        {
            get { return _service; }
            set
            {
                _service = value;
                Ticket = CalcTicketID();
            }
        }
        public int Residu
        {
            get { return _residu; }
            set
            {
                _residu = value;
                Ticket = CalcTicketID();
            }
        }
        public int Conteneur
        {
            get { return _conteneur; }
            set
            {
                _conteneur = value;
                Ticket = CalcTicketID();
            }
        }
        public int Operateur
        {
            get { return _operateur; }
            set
            {
                _operateur = value;
                Ticket = CalcTicketID();
            }
        }
        public int Ticket
        {
            get { return _ticket; }
            set
            {
                _ticket = value;
                _label.Text = ToString();
            }
        }
        public Label Label
        { get { return _label; } set { _label = value; } }


        public CodeBarre()
        {
            _client = 0;
            _service = 0;
            _residu = 0;
            _conteneur = 0;
            _operateur = 0;
            _ticket = 0;
        }
        //toString
        public override string ToString()
        {
            return $"{_client:00}{_service:00}{_residu:00}{_conteneur:00}{_operateur:00}{_ticket:000000}";
        }

        public int CalcTicketID()
        {
            return new EtiquetteTableAdapter().ticketNumber(
                $"%{_client:00}{_service:00}{_residu:00}{_conteneur:00}{_operateur:00}%") ?? 0;
        }
    }
}
