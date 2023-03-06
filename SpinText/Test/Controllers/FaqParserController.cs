﻿using DobriyCoder.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using SpinText.FaqParser;

namespace SpinText.Test.Controllers;

public class FaqParserController : Controller
{
    public string Index([FromServices] IFaqParser parser)
    {
        var html = @"<faq><item><question><h3>How to buy [coin1_long_name] with [coin2_long_name] at LetsExchange.io1.1?</h3></question><answer><p>{Buying|Purchasing} [coin1_long_name] with [coin2_long_name] on LetsExchange may be the {best|coolest|greatest} customer experience for such {transactions|operations}. There are only {a few|three} {steps|moves|operations} that have to be done: {choose|pick|select} the {fiat|traditional} currency you will {pay with|send}, {select|pick|set} the {crypto|coin|cryptocurrency} you {wish|are planning|want} to {buy|purchase|get}, {put in|enter|set|type in} the {sum|amount}, and click the ‘Buy now’ button. {After that|Then|Next} {follow|perform} {simple|uncomplicated} instructions to {get|receive|obtain} your [coin1_long_name] ([coin1_short_name]).</p></answer></item><item><question><h3>How to buy [coin1_long_name] with [coin2_long_name] at LetsExchange.io1.2?</h3></question><answer><p>{Buying|Purchasing} [coin1_long_name] with [coin2_long_name] on LetsExchange may be the {best|coolest|greatest} customer experience for such {transactions|operations}. There are only {a few|three} {steps|moves|operations} that have to be done: {choose|pick|select} the {fiat|traditional} currency you will {pay with|send}, {select|pick|set} the {crypto|coin|cryptocurrency} you {wish|are planning|want} to {buy|purchase|get}, {put in|enter|set|type in} the {sum|amount}, and click the ‘Buy now’ button. {After that|Then|Next} {follow|perform} {simple|uncomplicated} instructions to {get|receive|obtain} your [coin1_long_name] ([coin1_short_name]).</p></answer></item><item><question><h3>How to buy [coin1_long_name] with [coin2_long_name] at LetsExchange.io1.3?</h3></question><answer><p>{Buying|Purchasing} [coin1_long_name] with [coin2_long_name] on LetsExchange may be the {best|coolest|greatest} customer experience for such {transactions|operations}. There are only {a few|three} {steps|moves|operations} that have to be done: {choose|pick|select} the {fiat|traditional} currency you will {pay with|send}, {select|pick|set} the {crypto|coin|cryptocurrency} you {wish|are planning|want} to {buy|purchase|get}, {put in|enter|set|type in} the {sum|amount}, and click the ‘Buy now’ button. {After that|Then|Next} {follow|perform} {simple|uncomplicated} instructions to {get|receive|obtain} your [coin1_long_name] ([coin1_short_name]).</p></answer></item></faq><h3>How to buy [coin1_short_name] with [coin2_short_name] by credit or debit card?</h3><p>Buying [coin1_short_name] with [coin2_short_name] using a credit (debit) card is the {fastest|quickest} and {cheapest|most affordable} {way|method} to conduct such a {transaction|operation}. {But|However|Nonetheless}, you {need to|have to|should} make sure that your bank {allows|authorize|permit} such {deals|operations|transactions}.</p><h2>FAQ</h2><faq><item><question><h3>How to buy [coin1_long_name] with [coin2_long_name] at LetsExchange.io2.1?</h3></question><answer><p>{Buying|Purchasing} [coin1_long_name] with [coin2_long_name] on LetsExchange may be the {best|coolest|greatest} customer experience for such {transactions|operations}. There are only {a few|three} {steps|moves|operations} that have to be done: {choose|pick|select} the {fiat|traditional} currency you will {pay with|send}, {select|pick|set} the {crypto|coin|cryptocurrency} you {wish|are planning|want} to {buy|purchase|get}, {put in|enter|set|type in} the {sum|amount}, and click the ‘Buy now’ button. {After that|Then|Next} {follow|perform} {simple|uncomplicated} instructions to {get|receive|obtain} your [coin1_long_name] ([coin1_short_name]).</p></answer></item><item><question><h3>How to buy [coin1_long_name] with [coin2_long_name] at LetsExchange.io2.2?</h3></question><answer><p>{Buying|Purchasing} [coin1_long_name] with [coin2_long_name] on LetsExchange may be the {best|coolest|greatest} customer experience for such {transactions|operations}. There are only {a few|three} {steps|moves|operations} that have to be done: {choose|pick|select} the {fiat|traditional} currency you will {pay with|send}, {select|pick|set} the {crypto|coin|cryptocurrency} you {wish|are planning|want} to {buy|purchase|get}, {put in|enter|set|type in} the {sum|amount}, and click the ‘Buy now’ button. {After that|Then|Next} {follow|perform} {simple|uncomplicated} instructions to {get|receive|obtain} your [coin1_long_name] ([coin1_short_name]).</p></answer></item><item><question><h3>How to buy [coin1_long_name] with [coin2_long_name] at LetsExchange.io2.3?</h3></question><answer><p>{Buying|Purchasing} [coin1_long_name] with [coin2_long_name] on LetsExchange may be the {best|coolest|greatest} customer experience for such {transactions|operations}. There are only {a few|three} {steps|moves|operations} that have to be done: {choose|pick|select} the {fiat|traditional} currency you will {pay with|send}, {select|pick|set} the {crypto|coin|cryptocurrency} you {wish|are planning|want} to {buy|purchase|get}, {put in|enter|set|type in} the {sum|amount}, and click the ‘Buy now’ button. {After that|Then|Next} {follow|perform} {simple|uncomplicated} instructions to {get|receive|obtain} your [coin1_long_name] ([coin1_short_name]).</p></answer></item></faq>";
        var res = parser.Parse(html);
        res.PrintAsJson();
        return "";
    }
}
