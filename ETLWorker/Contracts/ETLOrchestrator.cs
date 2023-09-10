using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ETLWorker.Contracts
{

    public abstract class ETLOrchestrator<TInput, TOutput>
    {
        protected readonly IExtractor<TInput> extractor;
        protected readonly ITransformer<TInput, TOutput> transformer;
        protected readonly ILoader<TOutput> loader;

        public ETLOrchestrator(IExtractor<TInput> extractor, ITransformer<TInput, TOutput> transformer, ILoader<TOutput> loader)
        {
            this.extractor = extractor;
            this.transformer = transformer;
            this.loader = loader;
        }

        public void ExecuteETLProcess()
        {
            // Extract data
            List<TInput> extractedData = extractor.ExtractData();

            // Transform the data
            List<TOutput> transformedData = transformer.TransformData(extractedData);
            loader.LoadData(transformedData);
        }

        // Template method to define the ETL process steps
        public abstract void RunETLProcess();
    }


}
