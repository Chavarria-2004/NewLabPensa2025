using System;

class Program
{
    static void Main()
    {
        // Versión sin intro ni apodo y código base para la entrega del proyecto :)
        Console.Clear();

        // Mostrar instrucciones del juego al jugador
        Console.WriteLine("INSTRUCCIONES DEL JUEGO:");
        Console.WriteLine("\nEres el capitán de una nave espacial en un viaje intergaláctico.");
        Console.WriteLine("Debes gestionar combustible, oxígeno, suministros y la integridad de la nave");
        Console.WriteLine("para llegar a tu destino final en 10 días.");
        Console.WriteLine("\nCada día puedes elegir una acción:");
        Console.WriteLine("1. Explorar un planeta cercano (gasta combustible, puede encontrar recursos o peligros)");
        Console.WriteLine("2. Reparar la nave (gasta suministros para recuperar integridad)");
        Console.WriteLine("3. Enviar señales (puede atraer ayuda o problemas)");
        Console.WriteLine("4. Rendirse (GAME OVER)");
        Console.WriteLine("\nCada noche se consumen recursos y pueden ocurrir eventos aleatorios.");
        Console.WriteLine("\nPresiona ENTER para comenzar tu viaje...");
        Console.ReadLine();
        Console.Clear();

        // Inicialización de variables del juego
        int combustible = 30;       // Cantidad inicial de combustible
        int oxigeno = 50;           // Cantidad inicial de oxígeno
        int suministros = 40;       // Cantidad inicial de suministros
        int dia = 1;                // Día actual del viaje
        double integridadNave = 100; // Integridad inicial de la nave (100%)
        bool rendirse = false;      // Controla si el jugador se rinde
        bool accionRealizada;       // Indica si se completó una acción en el día
        Random aleatorio = new Random(); // Generador de números aleatorios

        Console.WriteLine("Prepárate para tu viaje intergaláctico!");

        // Bucle principal del juego (corre mientras haya días, recursos y no te rindas)
        while (dia <= 10 && combustible > 0 && oxigeno > 0 && integridadNave > 0 && !rendirse)
        {
            accionRealizada = false;
            
            // Bucle para realizar una acción cada día
            while (!accionRealizada)
            {
                // Mostrar estado actual
                Console.WriteLine("\n========== DÍA " + dia + " ==========");
                Console.WriteLine("Integridad: " + integridadNave + "% | Combustible: " + combustible + " | Oxígeno: " + oxigeno + " | Suministros: " + suministros);
                Console.WriteLine("¿Qué quieres hacer hoy?");
                Console.WriteLine("1. Explorar un planeta cercano");
                Console.WriteLine("2. Reparar la nave");
                Console.WriteLine("3. Enviar señales");
                Console.WriteLine("4. Rendirse F");

                // Validar entrada del jugador
                int opcion = 0;
                bool entradaValida = false;
                while (!entradaValida)
                {
                    Console.Write("Ingresa una opción (1-4): ");
                    string entrada = Console.ReadLine();
                    if (int.TryParse(entrada, out opcion) && opcion >= 1 && opcion <= 4)
                    {
                        entradaValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Opción inválida. Debe ser un número del 1 al 4.");
                    }
                }

                // Procesar opción seleccionada
                if (opcion == 1) // Explorar planeta
                {
                    if (combustible >= 15)
                    {
                        combustible -= 15; // Consumir combustible para explorar
                        double evento = aleatorio.NextDouble();
                        
                        // Eventos aleatorios al explorar
                        if (evento < 0.6) {
                            int ox = aleatorio.Next(20, 41);
                            oxigeno += ox;
                            Console.WriteLine($"¡Encontraste {ox} unidades de oxígeno!");
                        }
                        if (evento < 0.25) {
                            int comb = aleatorio.Next(10, 31);
                            combustible += comb;
                            Console.WriteLine($"¡Encontraste {comb} unidades de combustible!");
                        }
                        if (evento < 0.50) {
                            int sumin = aleatorio.Next(30, 101);
                            suministros += sumin;
                            Console.WriteLine($"¡Encontraste {sumin} suministros!");
                        }
                        if (evento < 0.25) {
                            int daño = aleatorio.Next(10, 21);
                            integridadNave -= daño;
                            Console.WriteLine($"¡Recibiste daño en la nave! -{daño}% de integridad.");
                        }
                        accionRealizada = true;
                    }
                    else
                    {
                        Console.WriteLine("No tienes suficiente combustible para explorar.");
                    }
                }
                else if (opcion == 2) // Reparar nave
                {
                    if (integridadNave >= 100)
                    {
                        Console.WriteLine("La nave ya está al 100%. No necesitas reparaciones.");
                    }
                    else
                    {
                        bool reparacionExitosa = false;
                        while (!reparacionExitosa)
                        {
                            Console.Write("Ingresa el % que quieres reparar (o escribe 'cancelar' para salir): ");
                            string entrada = Console.ReadLine();
                            if (entrada.ToLower() == "cancelar" || entrada == "0")
                            {
                                Console.WriteLine("Reparación cancelada.");
                                break;
                            }
                            if (int.TryParse(entrada, out int integridadReparada) && integridadReparada > 0)
                            {
                                int costo = integridadReparada * 10; // 10 suministros por 1% de reparación
                                if (suministros >= costo)
                                {
                                    if (integridadNave + integridadReparada > 100)
                                    {
                                        Console.WriteLine("Esa reparación supera el 100%. Intenta con menos.");
                                    }
                                    else
                                    {
                                        suministros -= costo;
                                        integridadNave += integridadReparada;
                                        Console.WriteLine("Reparación exitosa.");
                                        reparacionExitosa = true;
                                        accionRealizada = true;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No tienes suficientes suministros.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Entrada inválida. Usa números enteros positivos.");
                            }
                        }
                    }
                }
                else if (opcion == 3) // Enviar señales
                {
                    double prob = aleatorio.NextDouble();
                    if (prob < 0.5) // 50% de probabilidad de ayuda
                    {
                        combustible += 60;
                        Console.WriteLine("¡Ayuda recibida! Ganaste 60 de combustible.");
                    }
                    else // 50% de probabilidad de ataque
                    {
                        integridadNave -= 15;
                        suministros -= 20;
                        Console.WriteLine("¡Fuiste atacado por piratas! -15% integridad, -20 suministros.");
                    }
                    accionRealizada = true;
                }
                else if (opcion == 4) // Rendirse
                {
                    rendirse = true;
                    Console.WriteLine("Decidiste rendirte. Fin del juego.");
                    break;
                }
            }

            // Consumo nocturno de recursos
            if (!rendirse)
            {
                oxigeno -= 20;
                suministros -= 30;
                Console.WriteLine("\nDurante la noche perdiste 20 de oxígeno y 30 de suministros.");

                // Verificar si el jugador se quedó sin recursos
                if (oxigeno <= 0 || suministros <= 0 || integridadNave <= 0)
                {
                    Console.WriteLine("\nGAME OVER: Te quedaste sin recursos vitales durante la noche.");
                    break;
                }

                // Evento aleatorio nocturno (15% de probabilidad)
                if (aleatorio.NextDouble() <= 0.15)
                {
                    int evento = aleatorio.Next(3);
                    if (evento == 0) // Tormenta cósmica
                    {
                        oxigeno -= 10;
                        if (oxigeno < 0) oxigeno = 0;
                        Console.WriteLine("Una tormenta cósmica redujo tu oxígeno (-10).\n");
                    }
                    else if (evento == 1) // Encuentro alienígena
                    {
                        if (aleatorio.Next(2) == 0)
                        {
                            combustible += 20;
                            Console.WriteLine("Alienígenas amistosos te dieron 20 de combustible.");
                        }
                        else
                        {
                            integridadNave -= 10;
                            Console.WriteLine("Alienígenas hostiles dañaron tu nave (-10%).");
                        }
                    }
                    else // Meteoritos
                    {
                        Console.WriteLine("¡Meteoritos detectados!");
                        Console.WriteLine("¿Quieres maniobrar?");
                        Console.WriteLine("1. Sí");
                        Console.WriteLine("2. No");
                        int decision = 0;
                        bool val = false;
                        while (!val)
                        {
                            Console.Write("Ingresa 1 o 2: ");
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out decision) && (decision == 1 || decision == 2))
                            {
                                val = true;
                            }
                            else
                            {
                                Console.WriteLine("Opción inválida. Ingresa 1 o 2.");
                            }
                        }
                        if (decision == 1) // Esquivar meteoritos
                        {
                            int perd = aleatorio.Next(10, 31);
                            combustible -= perd;
                            Console.WriteLine($"Evitaste los meteoritos, pero perdiste {perd} de combustible.");
                        }
                        else // Recibir impacto
                        {
                            int daño = aleatorio.Next(15, 26);
                            integridadNave -= daño;
                            Console.WriteLine($"Recibiste el impacto y sufriste {daño}% de daño en la nave.");
                        }
                    }
                }

                // Verificar victoria al llegar al día 10
                if (dia == 10 && !rendirse && oxigeno > 0 && combustible > 0 && integridadNave > 0)
                {
                    Console.WriteLine("\n¡Felicidades Capitán! Llegaste a casa sano y salvo.");
                    break;
                }

                dia++; // Pasar al siguiente día
            }
        }

        // Mensaje final si perdiste por falta de recursos
        if (combustible <= 0 || oxigeno <= 0 || integridadNave <= 0)
        {
            Console.WriteLine("\nGAME OVER: Te quedaste sin recursos vitales.");
        }

        Console.WriteLine("\nGracias por jugar.");
    }
}