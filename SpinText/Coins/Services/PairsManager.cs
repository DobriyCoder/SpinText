using Microsoft.EntityFrameworkCore;
using SpinText.Coins.Models;
using SpinText.Models;

namespace SpinText.Coins.Services;

public class PairsManager
{
    CoinsManager _coins;

    public PairsManager(CoinsManager coins)
    {
        _coins = coins;
    }
}
