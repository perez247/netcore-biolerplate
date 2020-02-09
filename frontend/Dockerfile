# base image
FROM node as builder

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

RUN npm install -g @angular/cli@8.3.24

# Copy source code
COPY ./ ./

RUN npm install

# Create production application
RUN npm run prod

# Use nginx to serve application
FROM nginx:alpine

# expose port
EXPOSE 4200

# copy default.conf file
COPY ./default.conf /etc/nginx/conf.d/default.conf

# Copy ready prod file
COPY --from=builder /app/dist /usr/share/nginx/html
