﻿using Layers.Application.Models;
using Layers.Application.Requests;
using TestsExample.Models;

namespace Layers.Application.Helpers.MappingExtensions;

public static class PostMapping
{
    public static PostViewModel MapToPostViewModel(this Post post, string authorName)
    {
        return new()
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            Author = authorName,
            PublicationDateTime = post.PublicationDateTime
        };
    }

    public static Post MapToPost(this CreatePostRequest createPostRequest, Guid authorId)
    {
        return new()
        {
            Title = createPostRequest.Title,
            Content = createPostRequest.Content,
            PublicationDateTime = DateTime.UtcNow,
            UserId = authorId
        };
    }
}