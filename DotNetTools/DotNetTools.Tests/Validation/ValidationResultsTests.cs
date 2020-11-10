using Dataport.AppFrameDotNet.DotNetTools.Validation;
using Dataport.AppFrameDotNet.DotNetTools.Validation.Models;
using FluentAssertions;
using Xunit;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Validation
{
    public class ValidationResultsTests
    {
        [Fact]
        public void Ctr_Default_HasSeveritySuccess()
        {
            // act
            var result = new ValidationResults();

            // assert
            result.Severity.Should().Be(Severity.Success);
            result.Errors.Should().BeEmpty();
            result.Warnings.Should().BeEmpty();
            result.Information.Should().BeEmpty();
        }

        [Fact]
        public void AddInformation_BlankInstance_StoresMessageAndSetsSeverityToInfo()
        {
            // arrange
            var result = new ValidationResults();
            var message = "someMessage";

            // act
            result.AddInformation(message);

            // assert
            result.Severity.Should().Be(Severity.Information);
            result.Errors.Should().BeEmpty();
            result.Warnings.Should().BeEmpty();
            result.Information.Should().ContainSingle(i => i == message);
        }

        [Fact]
        public void AddWarning_BlankInstance_StoresMessageAndSetsSeverityToWarning()
        {
            // arrange
            var result = new ValidationResults();
            var message = "someMessage";

            // act
            result.AddWarning(message);

            // assert
            result.Severity.Should().Be(Severity.Warning);
            result.Errors.Should().BeEmpty();
            result.Warnings.Should().ContainSingle(i => i == message);
            result.Information.Should().BeEmpty();
        }

        [Fact]
        public void AddError_BlankInstance_StoresMessageAndSetsSeverityToError()
        {
            // arrange
            var result = new ValidationResults();
            var message = "someMessage";

            // act
            result.AddError(message);

            // assert
            result.Severity.Should().Be(Severity.Error);
            result.Errors.Should().ContainSingle(i => i == message);
            result.Warnings.Should().BeEmpty();
            result.Information.Should().BeEmpty();
        }

        [Fact]
        public void AddInformation_InstanceWithError_StoresMessageButLeavesSeverity()
        {
            // arrange
            var result = new ValidationResults();
            var message = "someMessage";
            result.AddError("someErrorMessage");

            // act
            result.AddInformation(message);

            // assert
            result.Severity.Should().Be(Severity.Error);
            result.Errors.Should().ContainSingle();
            result.Warnings.Should().BeEmpty();
            result.Information.Should().ContainSingle(i => i == message);
        }

        [Fact]
        public void AddInformation_InstanceWithWarning_StoresMessageButLeavesSeverity()
        {
            // arrange
            var result = new ValidationResults();
            var message = "someMessage";
            result.AddWarning("someWarningMessage");

            // act
            result.AddInformation(message);

            // assert
            result.Severity.Should().Be(Severity.Warning);
            result.Errors.Should().BeEmpty();
            result.Warnings.Should().ContainSingle();
            result.Information.Should().ContainSingle(i => i == message);
        }

        [Fact]
        public void AddWarning_InstanceWithError_StoresMessageButLeavesSeverity()
        {
            // arrange
            var result = new ValidationResults();
            var message = "someMessage";
            result.AddError("someErrorMessage");

            // act
            result.AddWarning(message);

            // assert
            result.Severity.Should().Be(Severity.Error);
            result.Errors.Should().ContainSingle();
            result.Warnings.Should().ContainSingle(i => i == message);
            result.Information.Should().BeEmpty();
        }
    }
}