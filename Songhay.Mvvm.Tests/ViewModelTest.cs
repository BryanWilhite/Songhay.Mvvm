using Amido.NAuto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Songhay.Extensions;
using Songhay.GenericWeb.Models;
using Songhay.Mvvm.Extensions;
using Songhay.Mvvm.ViewModels;
using System.Linq;

namespace Songhay.Mvvm.Tests
{
    [TestClass]
    public class ViewModelTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void ShouldConstructViewModels()
        {
            var document = NAuto.AutoBuild<Document>().Construct().Build();
            var expected = document.ToString();
            this.TestContext.WriteLine("Document data:");
            this.TestContext.WriteLine(expected);

            var documentVM = (new DocumentViewModel()).With(document);
            var actual = documentVM.ToString();
            this.TestContext.WriteLine("Document VM data:");
            this.TestContext.WriteLine(actual);

            Assert.AreEqual(expected, actual, "The expected data are not here");

            var fragment = NAuto.AutoBuild<Fragment>().Construct().Build();
            expected = fragment.ToString();
            this.TestContext.WriteLine("Fragment data:");
            this.TestContext.WriteLine(expected);

            var fragmentVM = (new FragmentViewModel()).With(fragment);
            actual = fragmentVM.ToString();
            this.TestContext.WriteLine("Fragment VM data:");
            this.TestContext.WriteLine(actual);

            Assert.AreEqual(expected, actual, "The expected data are not here");

            var segment = NAuto.AutoBuild<Segment>().Construct().Build();
            expected = document.ToString();
            this.TestContext.WriteLine("Segment data:");
            this.TestContext.WriteLine(expected);

            var segmentVM = (new SegmentViewModel()).With(segment);
            actual = documentVM.ToString();
            this.TestContext.WriteLine("Segment VM data:");
            this.TestContext.WriteLine(actual);

            Assert.AreEqual(expected, actual, "The expected data are not here");
        }

        [TestMethod]
        public void ShouldNotifyDocumentUpdates()
        {
            var document = NAuto.AutoBuild<Document>().Construct().Build();
            var documentVM = (new DocumentViewModel()).With(document).WithPropertyChangedNotifications(document);

            this.TestContext.WriteLine("Document data:");
            this.TestContext.WriteLine(document.ToString());

            this.TestContext.WriteLine("Document VM data:");
            this.TestContext.WriteLine(documentVM.ToString());

            documentVM.PropertyChanged += (s, args) => this.TestContext.WriteLine("PropertyChanged {0}: {1}",
                args.PropertyName, documentVM.GetPropertyValue(args.PropertyName));

            this.TestContext.WriteLine("updating Document.Title...");
            documentVM.Title = new string(documentVM.Title.ToArray().Reverse().ToArray());

            this.TestContext.WriteLine("Document data:");
            this.TestContext.WriteLine(document.ToString());

            this.TestContext.WriteLine("Document VM data:");
            this.TestContext.WriteLine(documentVM.ToString());

            Assert.AreEqual(documentVM.ToString(), document.ToString(), "The expected data is not here.");
        }

        [TestMethod]
        public void ShouldNotifyFragmentUpdates()
        {
            var fragment = NAuto.AutoBuild<Fragment>().Construct().Build();
            var fragmentVM = (new FragmentViewModel()).With(fragment).WithPropertyChangedNotifications(fragment);

            this.TestContext.WriteLine("fragment data:");
            this.TestContext.WriteLine(fragment.ToString());

            this.TestContext.WriteLine("fragment VM data:");
            this.TestContext.WriteLine(fragmentVM.ToString());

            fragmentVM.PropertyChanged += (s, args) => this.TestContext.WriteLine("PropertyChanged {0}: {1}",
                args.PropertyName, fragmentVM.GetPropertyValue(args.PropertyName));

            this.TestContext.WriteLine("updating fragment.IsWrapper...");
            fragmentVM.IsWrapper = !fragmentVM.IsWrapper;

            this.TestContext.WriteLine("fragment data:");
            this.TestContext.WriteLine(fragment.ToString());

            this.TestContext.WriteLine("fragment VM data:");
            this.TestContext.WriteLine(fragmentVM.ToString());

            Assert.AreEqual(fragmentVM.ToString(), fragment.ToString(), "The expected data is not here.");
        }

        [TestMethod]
        public void ShouldNotifySegmentUpdates()
        {
            var segment = NAuto.AutoBuild<Segment>().Construct().Build();
            var segmentVM = (new SegmentViewModel()).With(segment).WithPropertyChangedNotifications(segment);

            this.TestContext.WriteLine("segment data:");
            this.TestContext.WriteLine(segment.ToString());

            this.TestContext.WriteLine("segment VM data:");
            this.TestContext.WriteLine(segmentVM.ToString());

            segmentVM.PropertyChanged += (s, args) => this.TestContext.WriteLine("PropertyChanged {0}: {1}",
                args.PropertyName, segmentVM.GetPropertyValue(args.PropertyName));

            this.TestContext.WriteLine("updating segment.CreateDate...");
            segmentVM.CreateDate = segmentVM.CreateDate.GetValueOrDefault().AddYears(-4);

            this.TestContext.WriteLine("segment data:");
            this.TestContext.WriteLine(segment.ToString());

            this.TestContext.WriteLine("segment VM data:");
            this.TestContext.WriteLine(segmentVM.ToString());

            Assert.AreEqual(segmentVM.ToString(), segment.ToString(), "The expected data is not here.");
        }

        [TestMethod]
        public void ShouldValidateSegmentViewModel()
        {
            var vm = new SegmentViewModel();

            var errors = vm.Validate();
            Assert.IsFalse(vm.IsViewModelDataValid, "Validation errors are expected.");

            this.TestContext.WriteLine("Validation errors:");
            errors.ForEachInEnumerable(i =>
            {
                this.TestContext.WriteLine("{0}:", i.Key);
                i.Value.ForEachInEnumerable(j => this.TestContext.WriteLine(j));
            });
        }
    }
}
