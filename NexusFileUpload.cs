 string nexusUrl = _configuration["NexusUrl"];
                string nexusCredentials = _configuration["NexusCredentials"];
                string condoLifeRepositoryName = _configuration["CondoLifeNexusRepositoryName"];
                var client = new HttpClient();
                client.BaseAddress = new Uri(nexusUrl);
                var byteArray = Encoding.ASCII.GetBytes(nexusCredentials);
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                byte[] data;
                using (var br = new BinaryReader(file.OpenReadStream()))
                    data = br.ReadBytes((int) file.OpenReadStream().Length);

                ByteArrayContent bytes = new ByteArrayContent(data);
                HttpContent fileContent = bytes;
                var fileResult = client.PutAsync("repository/" + condoLifeRepositoryName + "/" + fileUrl, fileContent)
                    .Result;
