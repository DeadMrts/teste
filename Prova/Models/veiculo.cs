public class Veiculo
{
    public int Id { get; set; }
    public string Placa { get; set; }
    public string Modelo { get; set; }
    public DateTime HoraEntrada { get; set; }
    public DateTime? HoraSaida { get; set; }
    public bool Dentro { get; set; }
}
