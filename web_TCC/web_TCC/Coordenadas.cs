using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveBus
{
    public class Coordenadas
    {

        public double getPontoPelaCoordenada(string lat1, string lng1, string lat2, string lng2)
        {
            double origem_lat = Double.Parse(lat1.Replace(".", ","));
            double origem_lng = Double.Parse(lng1.Replace(".", ","));
            double destino_lat = Double.Parse(lat2.Replace(".", ","));
            double destino_lng = Double.Parse(lng2.Replace(".", ","));


            const int R = 6371; // Radius da terra
            Double latDistancia = deg2rad(destino_lat - origem_lat);
            Double lonDistancia = deg2rad(destino_lng - origem_lng);
            Double a = Math.Sin(latDistancia / 2) * Math.Sin(latDistancia / 2)
                    + Math.Cos(deg2rad(origem_lat)) * Math.Cos(deg2rad(destino_lat))
                    * Math.Sin(lonDistancia / 2) * Math.Sin(lonDistancia / 2);
            Double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distancia = R * c * 1000; // converter para metros
            distancia = Math.Pow(distancia, 2); 
            distancia = Math.Sqrt(distancia);

            return distancia;
        }

        public double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

    }
}