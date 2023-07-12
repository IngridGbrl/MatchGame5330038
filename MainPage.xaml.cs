
namespace MatchGame5330038;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Controls;
using System;
/// <summary>
/// <Creationdate>6/07/2023</Creationdate>
/// <Company>SANDBOX</Company>
/// <Lastmodificationdate>11/07/2023</Lastmodificationdate>
/// <Lastmodificationdescription>
/// se añadio un timer al juego en conjunto con un label y un boton para reiniciar
/// </Lastmodificationdescription>
/// <Lastmodificationautor>Ingrid Gabriel</Lastmodificationautor>
/// </summary>


public partial class MainPage : ContentPage
{
    /// <summary>
    /// Constructor de la clase
    /// </summary>
    
    IDispatcherTimer _timer;
    TimeSpan tiempoRestante; // Variable para almacenar el tiempo restante del juego

    public MainPage()
    {
        InitializeComponent();

        SetUpGame();
        setTime();

    }

    /// <summary>
    /// se crea un setTime donde se crea el timer y se usa Application.Current la cual es propiedad estática que devuelve 
    /// el objeto Application para el dominio de aplicación actual. Application.Current se 
    /// puede usar para obtener el Dispatcher asociado con el subproceso de interfaz de usuario principal
    /// </summary>
    public void setTime()
    {
       
        _timer =Application.Current.Dispatcher.CreateTimer();
        //se asigna los 10 segundos al timer
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += _timer_Tick;
        _timer.Start();
        // Inicializar el tiempo restante con 2 minutos
        tiempoRestante = TimeSpan.FromMinutes(2);
        // Asignar el valor inicial al texto del label con el formato mm:ss
        lbltimer.Text = tiempoRestante.ToString("mm\\:ss");
    }
    /// <summary>
    /// se crea el jjuego, el cual mostrara emojis los cuales estan dentro de una lista y usando random  los emojis apareceran
    /// aleatoriamente al cargar  con foreach se recorrera toda la lista de emojis que se encuentran en la lista
    /// </summary>
    private void SetUpGame()
	{
       

        List<string> animalEmoji=new List<string>()
		{
            "🐳","🐳",
            "🐹","🐹",
            "🐨","🐨",
            "🐱","🐱",
            "🐿️","🐿️",
            "🐣","🐣",
            "🐻","🐻",
            "🐰","🐰",

        };

		Random random = new Random();
		foreach(Button view in Grid1.Children )
		{
                int index = random.Next(animalEmoji.Count);

                string nextEmoji = animalEmoji[index];

                view.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            
        }
        
    }

	Button ultimoButtonClicked;

	bool encontrandoMatch = false;
    /// <summary>
    /// en este evento es donde se comparan los emojis si son iguales y son seleccionados se iran ocultando hasta
    /// encontrar el ultimo par de emojis con el if se verificara los pares sino son iguales entonces el boton del emoji 
    /// pulsado reaparecera
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private void Button_Clicked(object sender, EventArgs e)
    {
		Button button = sender as Button;
		if (encontrandoMatch == false) 
		{
			button.IsVisible = false;
			ultimoButtonClicked = button;
			encontrandoMatch= true;
		}
		else if(button.Text ==ultimoButtonClicked.Text) 
		{
			button.IsVisible = false;
			encontrandoMatch = false;
            
           
        }
		else
		{
			ultimoButtonClicked.IsVisible = true;
			encontrandoMatch = false;
           
        }
        
    }

    /// <summary>
    /// en este evento del timer se muestra el tiempo reestante del juego usando la propiedad tiemporestante que contiene timespan
    /// y asignando a un label con el formato que debe mostrar el timer y se crea un if el cual verifica si el tiempo ha finalizado
    /// mostrara un alert
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
   private void _timer_Tick(object sender, EventArgs e)
    {

        // se rest un segundo al tiempo restante
        tiempoRestante -= TimeSpan.FromSeconds(1);

        // leugo actualizamos el texto del label con el formato mm:ss
        lbltimer.Text = tiempoRestante.ToString("mm\\:ss");

        // si el tiempo se ha acabado, mostrar una alerta y detener el timer
        if (tiempoRestante == TimeSpan.Zero)
        {
            DisplayAlert("Match Game 5330038", "El tiempo se ha acabado", "Ok");
            _timer.Stop();
        }


    }


    private void Reinicio_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new MainPage());

    }


}

