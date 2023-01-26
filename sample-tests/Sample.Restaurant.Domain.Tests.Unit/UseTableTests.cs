﻿using FluentAssertions;
using Lewee.Domain;
using Xunit;

namespace Sample.Restaurant.Domain.Tests.Unit;

public sealed class UseTableTests
{
    private readonly Table target;

    public UseTableTests()
    {
        this.target = new Table(Guid.NewGuid(), 3);
    }

    [Fact]
    public void CanUseUnusedTable()
    {
        var correlationId = Guid.NewGuid();

        this.target.Use(correlationId);
        var domainEventsRaised = this.target.DomainEvents.GetAndClear();

        this.target.IsInUse.Should().BeTrue();
        this.target.CurrentOrder.Should().NotBeNull();

        domainEventsRaised.Should().NotBeNullOrEmpty();
        domainEventsRaised.Should().HaveCount(1);

        var domainEvent = domainEventsRaised[0];
        domainEvent.Should().BeOfType<TableInUseDomainEvent>();

        var tableInUseEvent = domainEvent as TableInUseDomainEvent;
        tableInUseEvent.Should().NotBeNull();
        tableInUseEvent.CorrelationId.Should().Be(correlationId);
        tableInUseEvent.TableId.Should().Be(this.target.Id);
        tableInUseEvent.TableNumber.Should().Be(this.target.TableNumber);
    }

    [Fact]
    public void CannotUseTableThatIsAlreadyInUse()
    {
        this.target.Use(Guid.NewGuid());

        var action = () => this.target.Use(Guid.NewGuid());

        action.Should().Throw<DomainException>()
            .WithMessage(Table.ErrorMessages.TableInUse);
    }
}
