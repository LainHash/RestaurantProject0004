using Microsoft.AspNetCore.WebUtilities;
using System.Reflection;

namespace Restaurant.Blazor.Common.Extensions
{
    /// <summary>
    /// Fluent builder dùng để ghép query string vào URL endpoint.
    /// </summary>
    public static class EndpointBuilder
    {
        // ─────────────────────────────────────────────────────────────
        // 1. Build từ object (tự động đọc properties bằng reflection)
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Ghép tất cả public properties của <paramref name="query"/> vào URL dưới dạng query string.
        /// Các giá trị <c>null</c>, chuỗi rỗng hoặc giá trị mặc định (0, false) sẽ bị bỏ qua.
        /// </summary>
        /// <example>
        /// var url = "api/product".AddQueryFrom(new ProductQuery { Keyword = "burger", Page = 2 });
        /// // → "api/product?Keyword=burger&amp;Page=2"
        /// </example>
        public static string AddQueryFrom<TQuery>(this string baseUrl, TQuery query)
            where TQuery : class
        {
            if (query is null) return baseUrl;

            var parameters = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

            foreach (var prop in typeof(TQuery).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = prop.GetValue(query);
                if (value is null) continue;

                var strValue = Convert.ToString(value);
                if (string.IsNullOrEmpty(strValue)) continue;

                // Bỏ qua số nguyên = 0 và bool = false (thường là giá trị không có ý nghĩa filter)
                if (value is int intVal && intVal == 0) continue;
                if (value is bool boolVal && !boolVal) continue;

                parameters[prop.Name] = strValue;
            }

            return QueryHelpers.AddQueryString(baseUrl, parameters);
        }

        // ─────────────────────────────────────────────────────────────
        // 2. Build từ Dictionary<string, string?>
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Ghép các cặp key-value trong <paramref name="parameters"/> vào URL.
        /// Bỏ qua các entry có value <c>null</c> hoặc rỗng.
        /// </summary>
        public static string AddQueryFrom(
            this string baseUrl,
            IDictionary<string, string?> parameters)
        {
            if (parameters is null || parameters.Count == 0) return baseUrl;

            var filtered = parameters
                .Where(kv => !string.IsNullOrWhiteSpace(kv.Value))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            return QueryHelpers.AddQueryString(baseUrl, filtered);
        }

        // ─────────────────────────────────────────────────────────────
        // 3. Thêm từng tham số đơn lẻ (Fluent chaining)
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Thêm một query param vào URL nếu <paramref name="value"/> có giá trị.
        /// </summary>
        /// <example>
        /// var url = "api/product"
        ///     .AddQuery("keyword", keyword)
        ///     .AddQuery("categoryId", categoryId)
        ///     .AddQuery("page", page.ToString());
        /// </example>
        public static string AddQuery(this string baseUrl, string key, string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return baseUrl;
            return QueryHelpers.AddQueryString(baseUrl, key, value);
        }

        /// <summary>
        /// Overload tiện lợi cho kiểu <c>int</c>; bỏ qua nếu value = 0.
        /// </summary>
        public static string AddQuery(this string baseUrl, string key, int value)
        {
            if (value == 0) return baseUrl;
            return QueryHelpers.AddQueryString(baseUrl, key, value.ToString());
        }

        /// <summary>
        /// Overload tiện lợi cho kiểu <c>int?</c>.
        /// </summary>
        public static string AddQuery(this string baseUrl, string key, int? value)
            => value.HasValue ? baseUrl.AddQuery(key, value.Value) : baseUrl;

        /// <summary>
        /// Overload tiện lợi cho kiểu <c>bool</c>; chỉ thêm khi value = <c>true</c>.
        /// </summary>
        public static string AddQuery(this string baseUrl, string key, bool value)
        {
            if (!value) return baseUrl;
            return QueryHelpers.AddQueryString(baseUrl, key, "true");
        }

        /// <summary>
        /// Overload tiện lợi cho kiểu <c>bool?</c>.
        /// </summary>
        public static string AddQuery(this string baseUrl, string key, bool? value)
            => value.HasValue ? baseUrl.AddQuery(key, value.Value) : baseUrl;

        /// <summary>
        /// Overload tiện lợi cho kiểu <c>Guid</c>; bỏ qua nếu value = <c>Guid.Empty</c>.
        /// </summary>
        public static string AddQuery(this string baseUrl, string key, Guid value)
        {
            if (value == Guid.Empty) return baseUrl;
            return QueryHelpers.AddQueryString(baseUrl, key, value.ToString());
        }

        /// <summary>
        /// Overload tiện lợi cho kiểu <c>Guid?</c>.
        /// </summary>
        public static string AddQuery(this string baseUrl, string key, Guid? value)
            => value.HasValue ? baseUrl.AddQuery(key, value.Value) : baseUrl;

        /// <summary>
        /// Overload tiện lợi cho kiểu <c>decimal</c>; bỏ qua nếu value = 0.
        /// </summary>
        public static string AddQuery(this string baseUrl, string key, decimal value)
        {
            if (value == 0m) return baseUrl;
            return QueryHelpers.AddQueryString(baseUrl, key, value.ToString());
        }

        // ─────────────────────────────────────────────────────────────
        // 4. Thêm nhiều giá trị cho cùng một key (multi-value)
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Thêm nhiều giá trị cho cùng một query key (ví dụ: ids=1&amp;ids=2&amp;ids=3).
        /// </summary>
        public static string AddQueryList<T>(this string baseUrl, string key, IEnumerable<T>? values)
        {
            if (values is null) return baseUrl;

            var url = baseUrl;
            foreach (var val in values)
            {
                var strVal = Convert.ToString(val);
                if (!string.IsNullOrEmpty(strVal))
                    url = QueryHelpers.AddQueryString(url, key, strVal);
            }

            return url;
        }
    }
}

