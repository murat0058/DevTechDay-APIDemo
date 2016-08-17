using Ecom.API.Entities;
using Ecom.API.Infrastructures.Extensions;
using Ecom.API.Repository.Abstract;
using Ecom.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Collections.Generic;
using System.Net;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")] //Attribute routing with routing token
    [Produces("application/json")] //Content Negotiation or filtering
    [Consumes("application/json", "application/json-patch+json")]
    [Authorize]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <remarks>
        /// This API gives list of all products.
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {
                var products = _productRepository.GetAll();
                var mappedProducts = AutoMapper.Mapper.Map<IEnumerable<Models.Product>>(products);

                return StatusCode((int)HttpStatusCode.OK, mappedProducts);
            }
            catch (Exception ex)
            {
                //You can also log exception "ex" here
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server error.");
            }
        }

        /// <summary>
        /// Get Product by code
        /// </summary>
        /// <remarks>Get Product by Code.</remarks>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpGet("{productCode}")]
        public IActionResult Get(string productCode)
        {
            try
            {
                var product = _productRepository.GetSingle(p => p.ProductCode == productCode);
                var mappedProduct = AutoMapper.Mapper.Map<Models.Product>(product);

                return StatusCode((int)HttpStatusCode.OK, mappedProduct);
            }
            catch (Exception ex)
            {
                //You can also log exception "ex" here
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server error.");
            }
        }


        /// <summary>
        /// Create new Product
        /// </summary>
        /// <remarks>Create new product.</remarks>
        /// <param name="product"></param>
        /// <returns>New Product.</returns>
        /// <response code = "201">Product Created.</response>
        /// <response code = "400">Unable to create product.</response>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, "Returns the newly created product.", typeof(Product))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "If the item is null")]
        public IActionResult Post([FromBody]ProductVM product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ModelState.GetErrors());
                }

                var productToPost = AutoMapper.Mapper.Map<Entities.Product>(product);

                _productRepository.Add(productToPost);
                _productRepository.Commit();

                return CreatedAtAction("Post", new { productCode = product.ProductCode }, productToPost);
            }
            catch (Exception ex)
            {
                //You can also log exception "ex" here
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server error.");
            }
        }


        /// <summary>
        /// Partial update of Product
        /// </summary>
        /// <remarks>Partial update of product</remarks>
        /// <param name="patchDocument"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult Patch([FromBody]JsonPatchDocument<ProductVM> patchDocument, [FromQuery]string productCode)
        {
            try
            {
                var producttToPatch = _productRepository.GetSingle(p => p.ProductCode == productCode);

                if (producttToPatch == null)
                {
                    return NotFound("Product not found.");
                }

                var productModel = AutoMapper.Mapper.Map<ProductVM>(producttToPatch);

                //Patching the changes
                patchDocument.ApplyTo(productModel);

                producttToPatch = AutoMapper.Mapper.Map<Entities.Product>(productModel);

                _productRepository.Update(producttToPatch);
                _productRepository.Commit();

                return StatusCode((int)HttpStatusCode.OK, producttToPatch);
            }
            catch (Exception ex)
            {
                //You can also log exception "ex" here
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server error.");
            }
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <remarks>Delete Product</remarks>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpDelete("{productCode}")]
        public IActionResult Delete(string productCode)
        {
            try
            {
                _productRepository.DeleteWhere(p => p.ProductCode == productCode);
                _productRepository.Commit();

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //You can also log exception "ex" here
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server error.");
            }
        }
    }
}