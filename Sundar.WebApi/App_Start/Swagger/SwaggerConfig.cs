using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using System.Reflection;
using Sundar.WebApi;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Sundar.WebApi
{
    /// <summary>
    /// Swagger������
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// ע��
        /// </summary>
        public static void Register()
        {
            var _thisAssembly = typeof(SwaggerConfig).Assembly;
            var _project = MethodBase.GetCurrentMethod().DeclaringType.Namespace;//��Ŀ�����ռ�
            var _xmlPath = string.Format("{0}/bin/{1}.XML", System.AppDomain.CurrentDomain.BaseDirectory, _project);
            var _jsPath = string.Format("{0}.Scripts.swaggerui.swagger_lang.js", _project);

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "API�ӿ��ĵ�");
                        c.IncludeXmlComments(_xmlPath);
                        c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider, _xmlPath));
                    })
                .EnableSwaggerUi(c =>
                    {
                        //��չjs  ·������:��Ŀ�����ռ�.�ļ�������.js�ļ�����
                        c.InjectJavaScript(_thisAssembly, _jsPath);
                        //Ĭ����ʾ�б�
                        c.DocExpansion(DocExpansion.List);
                    });
        }
    }
}
