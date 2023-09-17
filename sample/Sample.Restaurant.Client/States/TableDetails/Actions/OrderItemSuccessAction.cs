﻿using Lewee.Blazor.Fluxor.Actions;

namespace Sample.Restaurant.Client.States.TableDetails.Actions;

public record OrderItemSuccessAction : IRequestSuccessAction
{
    public string RequestType => "OrderItem";
}
