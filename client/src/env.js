export const dev = window.location.origin.includes('localhost')

// REVIEW don't forget to change this URL if you're using a dotnet backend
export const baseURL = dev ? 'https://localhost:7045' : 'http://localhost:3000'
export const useSockets = false
export const domain = 'dev-cucsliboqrmpiibf.us.auth0.com'
export const clientId = '9IHCopOWsypgrZC9Nniolr8lpzQ6Xvig'
export const audience = 'https://GregsList.com'
