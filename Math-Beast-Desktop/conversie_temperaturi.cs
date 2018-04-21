namespace Math_Beast_Desktop
{
    class conversie_temperaturi
    {
        internal string celsius_to_kelvin(double t)
        {
            return (t + 273.15).ToString();
        }

        internal string celsius_to_fahrenheit(double t)
        {
            return (t * 1.8 + 32).ToString();
        }

        internal string kelvin_to_celsius(double t)
        {
            return (t - 273.15).ToString();
        }

        internal string kelvin_to_fahrenheit(double t)
        {
            return (t * 1.8 - 459.67).ToString();
        }

        internal string fahrenheit_to_celsius(double t)
        {
            return ((t - 32) / 1.8).ToString();
        }

        internal string fahrenheit_to_kelvin(double t)
        {
            return ((t + 459.67) * 5 / 9).ToString();
        }
    }
}
